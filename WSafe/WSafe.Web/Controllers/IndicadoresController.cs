using Rotativa;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using WSafe.Domain.Data.Entities;
using WSafe.Domain.Helpers;
using WSafe.Domain.Helpers.Implements;
using WSafe.Web.Filters;
using WSafe.Web.Models;

namespace WSafe.Web.Controllers
{
    public class IndicadoresController : Controller
    {
        // Indicadores del SG-SST
        private int _clientID;
        private int _orgID;
        private string _year;
        private string _path;
        private readonly EmpresaContext _empresaContext;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IChartHelper _chartHelper;
        private readonly IIndicadorHelper _indicadorHelper;
        private readonly IGestorHelper _gestorHelper;
        public IndicadoresController
            (
                EmpresaContext empresaContext,
                IComboHelper comboHelper,
                IConverterHelper converterHelper,
                IChartHelper chartHelper,
                IIndicadorHelper indicadorHelper,
                IGestorHelper gestorHelper
            )
        {
            _empresaContext = empresaContext;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
            _chartHelper = chartHelper;
            _indicadorHelper = indicadorHelper;
            _gestorHelper = gestorHelper;

        }

        [AuthorizeUser(operation: 1, component: 5)]
        public ActionResult Index()
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                int year = Convert.ToInt32(_year);
                var month = DateTime.Now.Month;
                var model = _indicadorHelper.GetIndicators(year, month, _orgID);
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Indicadores", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetAllIndicators(int year)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                //int year = Convert.ToInt32(_year);
                var message = "";
                int month = 12;
                var model = _indicadorHelper.GetIndicators(year, month, _orgID);

                if (model == null)
                {
                    message = "La consulta No produjo ningún resultado !!";
                }
                return Json(new { data = model, mensaj = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Indicadores", "Index"));
            }
        }
        public ActionResult FrecuenciaAccidentalidad(string periodo, int year)
        {
            return View("Details");
        }
        public ActionResult SeveridadAccidentes(string periodo, int year)
        {
            var result = new CreateIndicatorsViewModel();
            result.Indicadores = _comboHelper.GetComboIndicadores();
            ViewBag.Image = "~/Images/chart05.jpg";
            ViewBag.Titulo = "FICHA TÉCNICA INDICADOR SEVERIDAD ACCIDENTALIDAD";
            ViewBag.Periodo = periodo;
            ViewBag.Year = year;
            return View("Details");
        }
        public ActionResult ProrcionAccidentesMortales(string periodo, int year)
        {
            var result = new CreateIndicatorsViewModel();
            result.Indicadores = _comboHelper.GetComboIndicadores();
            ViewBag.Image = "~/Images/chart05.jpg";
            ViewBag.Titulo = "FICHA TÉCNICA INDICADOR PROPORCION DE ACCIDENTES MORTALES";
            ViewBag.Periodo = periodo;
            ViewBag.Year = year;
            return View("Details");
        }
        public ActionResult Accidentabilidad(string periodo, int year)
        {
            var result = new CreateIndicatorsViewModel();
            result.Indicadores = _comboHelper.GetComboIndicadores();
            ViewBag.Image = "~/Images/chart05.jpg";
            ViewBag.Titulo = "FICHA TÉCNICA INDICADOR ACCIDENTABILIDAD";
            ViewBag.Periodo = periodo;
            ViewBag.Year = year;
            return View("Details");
        }
        public ActionResult ProporcionAccionesEjecutadas(string periodo, int year)
        {
            var result = new CreateIndicatorsViewModel();
            result.Indicadores = _comboHelper.GetComboIndicadores();
            ViewBag.Image = "~/Images/chart05.jpg";
            ViewBag.Titulo = "FICHA TÉCNICA INDICADOR PROPORCION DE ACCIONES EJECUTADAS";
            ViewBag.Periodo = periodo;
            ViewBag.Year = year;
            return View("Details");
        }
        public ActionResult ProporcionAccionesEfectivas(string periodo, int year)
        {
            var result = new CreateIndicatorsViewModel();
            result.Indicadores = _comboHelper.GetComboIndicadores();
            ViewBag.Image = "~/Images/chart05.jpg";
            ViewBag.Titulo = "FICHA TÉCNICA INDICADOR PROPORCION DE ACCIONES EFECTIVAS";
            ViewBag.Periodo = periodo;
            ViewBag.Year = year;
            return View("Details");
        }
        public ActionResult DetailsPDF(string periodo, int year, string image, string titulo, int id)
        {
            _orgID = (int)Session["orgID"];
            var result = _empresaContext.Incidentes.FirstOrDefault(i => i.ID == id);
            var modelo = _converterHelper.ToIncidenteViewModel(result, _orgID);
            var document = _empresaContext.Documents.FirstOrDefault(d => d.ID == id);
            ViewBag.formato = document.Formato;
            ViewBag.estandar = document.Estandar;
            ViewBag.titulo = document.Titulo;
            ViewBag.version = document.Version;
            ViewBag.fecha = DateTime.Now;
            ViewBag.Image = image;
            ViewBag.Titulo = titulo;
            ViewBag.Periodo = periodo;
            ViewBag.Year = year;
            return View(modelo);
        }

        [HttpGet]
        public ActionResult FrecuenciaAccidentalidad(int year)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var month = 12;
                if (year == DateTime.Now.Year)
                {
                    month = DateTime.Now.Month;
                }
                int[] periodo = new int[month];
                for (int i = 0; i < month; i++)
                {
                    periodo[i] = i + 1;
                }

                var datos = _chartHelper.GetFrecuenciaAccidentes(periodo, year, _orgID);

                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
            }
        }

        [HttpGet]
        public ActionResult SeveridadAccidentalidad(int year)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var month = 12;
                if (year == DateTime.Now.Year)
                {
                    month = DateTime.Now.Month;
                }
                int[] periodo = new int[month];
                for (int i = 0; i < month; i++)
                {
                    periodo[i] = i + 1;
                }

                var datos = _chartHelper.GetSeveridadAccidentalidad(periodo, year, _orgID);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
            }
        }

        [HttpGet]
        public ActionResult MortalityProportion(int year)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var month = 12;
                if (year == DateTime.Now.Year)
                {
                    month = DateTime.Now.Month;
                }
                int[] periodo = new int[month];
                for (int i = 0; i < month; i++)
                {
                    periodo[i] = i + 1;
                }

                var datos = _chartHelper.GetAccidentesTrabajoMortales(periodo, year, _orgID);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Riesgos", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetAusentismo(int year)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var month = 12;
                if (year == DateTime.Now.Year)
                {
                    month = DateTime.Now.Month;
                }
                int[] periodo = new int[month];
                for (int i = 0; i < month; i++)
                {
                    periodo[i] = i + 1;
                }

                var datos = _chartHelper.GetAusentismoCausaMedica(periodo, year);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Indicadores", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetFatorRiesgoOcupacional(int year)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var datos = _chartHelper.GetFatorRiesgoOcupacional(year, _orgID);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Indicadores", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetActionsIndicator(int year)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var datos = _chartHelper.GetAllValueCorrectiveActions(year, _orgID);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Indicadores", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetScholarship(int year)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var datos = _chartHelper.GetAllScholarship(year, _orgID);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Indicadores", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetWorkAreas(int year)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var datos = _chartHelper.GetAllWorkAreas(year, _orgID);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Indicadores", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetOcupaciones(int year)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var datos = _chartHelper.GetAllOcupaciones(year, _orgID);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Indicadores", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetTiposVinculacion(int year)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var datos = _chartHelper.GetAllTiposVinculacion(year, _orgID);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Indicadores", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetEstadosCivil(int year)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var datos = _chartHelper.GetAllEstadosCivil(year, _orgID);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Indicadores", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetTiposVivienda(int year)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var datos = _chartHelper.GetAllTiposVivienda(year, _orgID);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Indicadores", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetTiposJornada(int year)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var datos = _chartHelper.GetAllTiposJornada(year, _orgID);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Indicadores", "Index"));
            }
        }

        [HttpGet]
        public ActionResult GetNumeroHijos(int year)
        {
            try
            {
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                Random random = new Random();
                var filename = "chart" + random.Next(1, 100) + ".jpg";
                var filePathName = "~/Images/" + filename;
                var datos = _chartHelper.GetAllNumeroHijos(year, _orgID);
                var image = "/Images/" + filename;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Indicadores", "Index"));
            }
        }

        [HttpGet]
        public async Task<ActionResult> PrintIndicatorToPdf(int id, int Year)
        {
            try
            {
                // Configuración nombre archivo pdf
                _clientID = (int)Session["clientID"];
                _orgID = (int)Session["orgID"];
                _year = (string)Session["year"];
                _path = (string)Session["path"];
                var indicador = await _empresaContext.Indicadores.FirstOrDefaultAsync(d => d.ID == id);
                var document = await _empresaContext.Documents.FirstOrDefaultAsync(d => d.ID == id);
                var item = indicador.Standard.Trim();
                var norma = await _empresaContext.Normas.FirstOrDefaultAsync(n => n.Item == item);
                var organization = _empresaContext.Organizations.Find(_orgID);
                var year = _year;
                var fullPath = $"{_path}/2. HACER/{item}/";
                var path = Server.MapPath(fullPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (Year != 0)
                {
                    var month = 12;
                    if (Year == DateTime.Now.Year)
                    {
                        month = DateTime.Now.Month;
                    }
                    int[] periodo = new int[month];
                    for (int i = 0; i < month; i++)
                    {
                        periodo[i] = i + 1;
                    }

                    Random random = new Random();
                    var filename = indicador.Nombre.Trim() + random.Next(1, 100) + ".Pdf";
                    var filePathName = path + filename;
                    var imagePathName = "~/Images/" + filename;
                    var imageName = Server.MapPath(imagePathName);
                    var result = _converterHelper.ToIndicadorViewModelNew(
                        indicador, periodo, Year, _orgID, imageName);
                    ViewBag.txtPeriodo = _gestorHelper.GetPeriodo(periodo);
                    ViewBag.Year = year.ToString();
                    ViewBag.id = result.ID;
                    ViewBag.formato = document.Formato;
                    ViewBag.estandar = indicador.Standard;
                    ViewBag.Titulo = result.Titulo;
                    ViewBag.version = document.Version;
                    ViewBag.fecha = DateTime.Now;
                    var report = new ViewAsPdf("Details");
                    report.Model = result;
                    report.FileName = filePathName;
                    report.PageSize = Rotativa.Options.Size.A4;
                    report.Copies = 1;
                    report.PageOrientation.GetValueOrDefault();
                    report.FormsAuthenticationCookieName = FormsAuthentication.FormsCookieName;
                    report.SaveOnServerPath = filePathName;

                    //Generar archivo de movimiento
                    var fullName = filename;
                    var type = Path.GetExtension(filename).ToUpper();
                    var descript = "Reporte Indicadores";
                    var userID = (int)Session["userID"];
                    Movimient movimient = new Movimient()
                    {
                        ID = 0,
                        OrganizationID = _orgID,
                        NormaID = norma.ID,
                        UserID = userID,
                        Descripcion = descript,
                        Document = fullName,
                        Year = year,
                        Item = item,
                        Ciclo = "H",
                        Type = type,
                        Path = path,
                        ClientID = _clientID
                    };
                    _empresaContext.Movimientos.Add(movimient);
                    await _empresaContext.SaveChangesAsync();
                    return report;
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Indicadores", "Index"));
            }
        }
    }
}