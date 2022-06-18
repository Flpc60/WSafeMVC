var tabla = "";
var tabla1 = "";
var tabla2 = "";
var selectedRow = null
var categoria = null

function guardarProceso(codigo, nombre, actividad, id, idtabla) {
  tabla = document.getElementById(idtabla);
  tabla.length = 1;
  var hilera = "";
  var celda = "";
  var textoCelda = "";

  // Columna 1
  hilera = document.createElement("tr");
  celda = document.createElement("td");
  textoCelda = document.createTextNode(codigo.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  celda = document.createElement("td");
  textoCelda = document.createTextNode(nombre.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  celda = document.createElement("td");
  textoCelda = document.createTextNode(actividad.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  document.getElementById(id).style.display = "none";
  resetForm();
}

function guardarAct(codigo, nombre, archivo, observa, id, idtabla) {
  tabla = document.getElementById(idtabla);
  tabla.length = 1;
  var hilera = "";
  var celda = "";
  var textoCelda = "";

  // Columna 1
  hilera = document.createElement("tr");
  celda = document.createElement("td");
  textoCelda = document.createTextNode(codigo.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  celda = document.createElement("td");
  textoCelda = document.createTextNode(nombre.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  celda = document.createElement("td");
  textoCelda = document.createTextNode(archivo.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  celda = document.createElement("td");
  textoCelda = document.createTextNode(observa.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  document.getElementById(id).style.display = "none";
  resetForm();
}

function guardarCentro(codigo, nombre, id, idtabla) {
  tabla = document.getElementById(idtabla);
  tabla.length = 1;
  var hilera = "";
  var celda = "";
  var textoCelda = "";

  // Columna 1
  hilera = document.createElement("tr");
  celda = document.createElement("td");
  textoCelda = document.createTextNode(codigo.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  celda = document.createElement("td");
  textoCelda = document.createTextNode(nombre.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  document.getElementById(id).style.display = "none";
  resetForm();
}

function guardarSede(descripcion, id, idtabla) {
  tabla = document.getElementById(idtabla);
  tabla.length = 1;
  var hilera = "";
  var celda = "";
  var textoCelda = "";

  // Columna 1
  hilera = document.createElement("tr");
  celda = document.createElement("td");
  textoCelda = document.createTextNode(descripcion.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);
  document.getElementById(id).style.display = "none";
  resetForm();
}

function guardarTrabajador(
  apellido1, apellido2, nombres, documento, fechaNacimiento, genero, estadoCivil, tipo, email,
  sede, cargo, centro, fechaIngreso, eps, afp, arl, address, phone, riesgo, actividad, id, idtabla) {
    tabla = document.getElementById(idtabla);
    tabla.length = 1;
    var hilera = "";
    var celda = "";
    var textoCelda = "";
  
    // Columna 1
    hilera = document.createElement("tr");
    celda = document.createElement("td");
    textoCelda = document.createTextNode(apellido1.value);
    celda.appendChild(textoCelda);
    hilera.appendChild(celda);
    tabla.appendChild(hilera);
  
    // Columna 2
    celda = document.createElement("td");
    textoCelda = document.createTextNode(apellido2.value);
    celda.appendChild(textoCelda);
    hilera.appendChild(celda);
    tabla.appendChild(hilera);
  
    // Columna 3
    celda = document.createElement("td");
    textoCelda = document.createTextNode(nombres.value);
    celda.appendChild(textoCelda);
    hilera.appendChild(celda);
    tabla.appendChild(hilera);

    // Columna 4
    celda = document.createElement("td");
    textoCelda = document.createTextNode(documento.value);
    celda.appendChild(textoCelda);
    hilera.appendChild(celda);
    tabla.appendChild(hilera);
  
    // Columna 5
    celda = document.createElement("td");
    textoCelda = document.createTextNode(fechaNacimiento.value);
    celda.appendChild(textoCelda);
    hilera.appendChild(celda);
    tabla.appendChild(hilera);
  
    // Columna 6
    celda = document.createElement("td");
    textoCelda = document.createTextNode(genero.value);
    celda.appendChild(textoCelda);
    hilera.appendChild(celda);
    tabla.appendChild(hilera);
  
    // Columna 7
    celda = document.createElement("td");
    textoCelda = document.createTextNode(estadoCivil.value);
    celda.appendChild(textoCelda);
    hilera.appendChild(celda);
    tabla.appendChild(hilera);
  
    // Columna 8
    celda = document.createElement("td");
    textoCelda = document.createTextNode(tipo.value);
    celda.appendChild(textoCelda);
    hilera.appendChild(celda);
    tabla.appendChild(hilera);
  
    // Columna 9
    celda = document.createElement("td");
    textoCelda = document.createTextNode(email.value);
    celda.appendChild(textoCelda);
    hilera.appendChild(celda);
    tabla.appendChild(hilera);
  
    // Columna 10
    celda = document.createElement("td");
    textoCelda = document.createTextNode(sede.value);
    celda.appendChild(textoCelda);
    hilera.appendChild(celda);
    tabla.appendChild(hilera);
  
    // Columna 11
    celda = document.createElement("td");
    textoCelda = document.createTextNode(cargo.value);
    celda.appendChild(textoCelda);
    hilera.appendChild(celda);
    tabla.appendChild(hilera);
  
    // Columna 12
    celda = document.createElement("td");
    textoCelda = document.createTextNode(centro.value);
    celda.appendChild(textoCelda);
    hilera.appendChild(celda);
    tabla.appendChild(hilera);
  
    // Columna 13
    celda = document.createElement("td");
    textoCelda = document.createTextNode(fechaIngreso.value);
    celda.appendChild(textoCelda);
    hilera.appendChild(celda);
    tabla.appendChild(hilera);
  
    // Columna 14
    celda = document.createElement("td");
    textoCelda = document.createTextNode(eps.value);
    celda.appendChild(textoCelda);
    hilera.appendChild(celda);
    tabla.appendChild(hilera);
  
    // Columna 15
    celda = document.createElement("td");
    textoCelda = document.createTextNode(afp.value);
    celda.appendChild(textoCelda);
    hilera.appendChild(celda);
    tabla.appendChild(hilera);
  
    // Columna 16
    celda = document.createElement("td");
    textoCelda = document.createTextNode(arl.value);
    celda.appendChild(textoCelda);
    hilera.appendChild(celda);
    tabla.appendChild(hilera);
  
    // Columna 17
    celda = document.createElement("td");
    textoCelda = document.createTextNode(address.value);
    celda.appendChild(textoCelda);
    hilera.appendChild(celda);
    tabla.appendChild(hilera);
  
    // Columna 18
    celda = document.createElement("td");
    textoCelda = document.createTextNode(phone.value);
    celda.appendChild(textoCelda);
    hilera.appendChild(celda);
    tabla.appendChild(hilera);
  
    // Columna 19
    celda = document.createElement("td");
    textoCelda = document.createTextNode(riesgo.value);
    celda.appendChild(textoCelda);
    hilera.appendChild(celda);
    tabla.appendChild(hilera);
  
    // Columna 20
    celda = document.createElement("td");
    textoCelda = document.createTextNode(actividad.value);
    celda.appendChild(textoCelda);
    hilera.appendChild(celda);
    tabla.appendChild(hilera);
    document.getElementById(id).style.display = "none";
    resetForm();
}

function guardarTraza(actividad, amenaza, control, categoria, fecha, responsable, emails, id, idtabla) {
  tabla = document.getElementById(idtabla);
  tabla.length = 1;
  var hilera = "";
  var celda = "";
  var textoCelda = "";

  // Columna 1
  hilera = document.createElement("tr");
  celda = document.createElement("td");
  textoCelda = document.createTextNode(actividad.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  // Columna 2
  celda = document.createElement("td");
  textoCelda = document.createTextNode(amenaza.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);
  
  // Columna 3
  celda = document.createElement("td");
  textoCelda = document.createTextNode(control.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);
  
  // Columna 4
  celda = document.createElement("td");
  textoCelda = document.createTextNode(categoria);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);
  
  // Columna 5
  celda = document.createElement("td");
  textoCelda = document.createTextNode(fecha.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);
  
  // Columna 6
  celda = document.createElement("td");
  textoCelda = document.createTextNode(responsable.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  
  // Columna 6
  celda = document.createElement("td");
  textoCelda = document.createTextNode(emails.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  document.getElementById(id).style.display = "none";
  resetForm();
}

function activaPeFisicos(){
  document.getElementById("peFisicos").style.display = "block";
  document.getElementById("peLocativos").style.display = "none";
  document.getElementById("peElectricos").style.display = "none";
  document.getElementById("peSeguridad").style.display = "none";
  document.getElementById("pe").style.display = "none";
}

function activaIdentificacion(){
  document.getElementById("identificacion").style.display = "block";
  document.getElementById("evaluacion").style.display = "none";
  document.getElementById("intervencion").style.display = "none";
}

function activaEvaluacion(){
  document.getElementById("identificacion").style.display = "none";
  document.getElementById("evaluacion").style.display = "block";
  document.getElementById("intervencion").style.display = "none";
}

function activaIntervencion(){
  document.getElementById("identificacion").style.display = "none";
  document.getElementById("evaluacion").style.display = "none";
  document.getElementById("intervencion").style.display = "block";
}

function guardarControl(descripcion, nombre, fInicial, fFinal, efectividad, observa, id, idtabla) {
  tabla = document.getElementById(idtabla);
  tabla.length = 1;
  var hilera = "";
  var celda = "";
  var textoCelda = "";

  // Columna 1
  hilera = document.createElement("tr");
  celda = document.createElement("td");
  textoCelda = document.createTextNode(descripcion.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  // Columna 2
  celda = document.createElement("td");
  textoCelda = document.createTextNode(nombre.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);
  
  // Columna 3
  celda = document.createElement("td");
  textoCelda = document.createTextNode(fInicial.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);
  
  // Columna 4
  celda = document.createElement("td");
  textoCelda = document.createTextNode(fFinal.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);
  
  // Columna 5
  celda = document.createElement("td");
  textoCelda = document.createTextNode(efectividad.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);
  
  // Columna 6
  celda = document.createElement("td");
  textoCelda = document.createTextNode(observa.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);
  document.getElementById(id).style.display = "none";
  resetForm();
}

function aplicarControlador(controlador, fInicial, trabajador, id, idtabla) {
  tabla = document.getElementById(idtabla);
  tabla.length = 1;
  var hilera = "";
  var celda = "";
  var textoCelda = "";

  // Columna 1
  hilera = document.createElement("tr");
  celda = document.createElement("td");
  textoCelda = document.createTextNode(descripcion.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  // Columna 2
  celda = document.createElement("td");
  textoCelda = document.createTextNode(nombre.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);
  
  // Columna 3
  celda = document.createElement("td");
  textoCelda = document.createTextNode(fInicial.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);
  
  // Columna 4
  celda = document.createElement("td");
  textoCelda = document.createTextNode(fFinal.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);
  
  // Columna 5
  celda = document.createElement("td");
  textoCelda = document.createTextNode(efectividad.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);
  
  // Columna 6
  celda = document.createElement("td");
  textoCelda = document.createTextNode(observa.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);
  document.getElementById(id).style.display = "none";
  resetForm();
}

function guardarControlador(codigo, nombre, categoria, valoracion, finalidad, trabajador, archivo, presupuesto, observa, id, idtabla) {
  tabla = document.getElementById(idtabla);
  tabla.length = 1;
  var hilera = "";
  var celda = "";
  var textoCelda = "";

  // Columna 1
  hilera = document.createElement("tr");
  celda = document.createElement("td");
  textoCelda = document.createTextNode(codigo.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  // Columna 2
  celda = document.createElement("td");
  textoCelda = document.createTextNode(nombre.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);
  
  // Columna 3
  celda = document.createElement("td");
  textoCelda = document.createTextNode(categoria.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);
  
  // Columna 3
  celda = document.createElement("td");
  textoCelda = document.createTextNode(valoracion.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);
  
  // Columna 4
  celda = document.createElement("td");
  textoCelda = document.createTextNode(finalidad.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);
  
  // Columna 4
  celda = document.createElement("td");
  textoCelda = document.createTextNode(trabajador.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  // Columna 5
  celda = document.createElement("td");
  textoCelda = document.createTextNode(archivo.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  // Columna 9
  celda = document.createElement("td");
  textoCelda = document.createTextNode(presupuesto.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  // Columna 10
  celda = document.createElement("td");
  textoCelda = document.createTextNode(observa.value);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  document.getElementById(id).style.display = "none";
  resetForm();
}

function copiaGuardarActividad(data, id, idtabla) {
  tabla = document.getElementById(idtabla);
  tabla.length = 1;
  var hilera = "";
  var celda = "";
  var textoCelda = "";

  // Columna 1
  hilera = document.createElement("tr");
  celda = document.createElement("td");
  textoCelda = document.createTextNode(data.codigoActividad);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  // Columna 2
  celda = document.createElement("td");
  textoCelda = document.createTextNode(data.nombreActividad);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  // Columna 3
  celda = document.createElement("td");
  textoCelda = document.createTextNode(data.archivo);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  // Columna 2
  celda = document.createElement("td");
  textoCelda = document.createTextNode(data.amenaza);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  // Columna 3
  celda = document.createElement("td");
  textoCelda = document.createTextNode(data.consecuencia);
  celda.appendChild(textoCelda);
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  celda = document.createElement("td");
  celda.innerHTML =`<a onclick="editarActividad(this)"><i class="fa fa-edit"></i>Editar</a>
                     <a onclick="borrarActividad(this)"><i class="fa fa-trash-alt"></i>Borrar</a>`;
  hilera.appendChild(celda);
  tabla.appendChild(hilera);

  document.getElementById(id).style.display = "none";
  resetForm();
}

function updateRecord(formData) {
  selectedRow.cells[0].innerHTML = formData.codigoActividad;
  selectedRow.cells[1].innerHTML = formData.nombreActividad;
  selectedRow.cells[2].innerHTML = formData.archivo;
  selectedRow.cells[3].innerHTML = formData.amenaza;
  selectedRow.cells[4].innerHTML = formData.consecuencia;
}

function editarActividad(td) {
  resetForm();
  selectedRow = td.parentElement.parentElement;
  document.getElementById("codigoActividad").value = selectedRow.cells[0].innerHTML;
  document.getElementById("nombreActividad").value = selectedRow.cells[1].innerHTML;
  //document.getElementById("archivo").value = selectedRow.cells[2].innerHTML;
  document.getElementById("amenaza").value = selectedRow.cells[3].innerHTML;
  document.getElementById("consecuencia").value = selectedRow.cells[4].innerHTML;
  document.getElementById("formActividad").style.display = "block";
  document.getElementById('controlador').style.display = 'none'  
}

function borrarActividad(td) {
  if (confirm('Esta seguro(a) de borrar esta actividad ?')) {
    row = td.parentElement.parentElement;
    document.getElementById("tabla1").deleteRow(row.rowIndex);
    resetForm();
  }
}

function editarPreventivo(td) {
  resetPreventivo();
  selectedRow = td.parentElement.parentElement;
  if(categoria == 'Preventivo') {
    document.getElementById("otraActividad").value = selectedRow.cells[0].innerHTML;
  }else {
    document.getElementById("actividad").value = selectedRow.cells[0].innerHTML;
  }
  document.getElementById("otraAmenaza").value = selectedRow.cells[1].innerHTML;
  document.getElementById("preventivo").value = selectedRow.cells[2].innerHTML;
  document.getElementById("fechaInicial").value = selectedRow.cells[3].innerHTML;
  document.getElementById("trabajador").value = selectedRow.cells[4].innerHTML;
  document.getElementById("formPreventivo").style.display = "block";
  document.getElementById('controlador1').style.display = 'none'  
}

function borrarPreventivo(td) {
  if (confirm('Esta seguro(a) de borrar este control ?')) {
    row = td.parentElement.parentElement;
    document.getElementById("tabla3").deleteRow(row.rowIndex);
    resetPreventivo();
  }
}

function guardarPreventivo(cat, id, idtabla) {
  categoria = cat;
  var formData = readPreventivo();
  if (selectedRow == null) {
    insertNewPreventivo(formData, idtabla);
  }
  else{
    updatePreventivo(formData);
  }
  resetPreventivo();
  document.getElementById(id).style.display='none';    
}

function readPreventivo() {
  var formData = {};
  if(categoria == "Preventivo") {
    formData["otraActividad"] = document.getElementById("otraActividad").value;
  }else {
    formData["actividad"] = document.getElementById("actividad").value;
  }
  formData["amenaza"] = document.getElementById("otraAmenaza").value;
  formData["preventivo"] = document.getElementById("preventivo").value;
  formData["fechaInicial"] = document.getElementById("fechaInicial").value;
  if(categoria == 'Preventivo') {
    formData["otroTrabajador"] = document.getElementById("otroTrabajador").value;
  }else {
    formData["trabajador"] = document.getElementById("trabajador").value;
  }
  formData["email"] = document.getElementById("email").value;
  return formData;
}

function insertNewPreventivo(data, idtabla) {
  tabla1 = document.getElementById(idtabla).getElementsByTagName('tbody')[0];
  newRow = tabla1.insertRow(tabla1.length);
  if(categoria == 'Preventivo') {
    cell1 = newRow.insertCell(0);
    cell1.innerHTML = data.otraActividad;
  }else {
    cell1 = newRow.insertCell(0);
    cell1.innerHTML = data.actividad;
  }
  cell2 = newRow.insertCell(1);
  cell2.innerHTML = data.amenaza;
  cell3 = newRow.insertCell(2);
  cell3.innerHTML = data.preventivo;
  cell4 = newRow.insertCell(3);
  cell4.innerHTML = data.fechaInicial;
  if(categoria == "Preventivo") {
    cell5 = newRow.insertCell(4);
    cell5.innerHTML = data.otroTrabajador;
  } else {
    cell5 = newRow.insertCell(4);
    cell5.innerHTML = data.trabajador;
  }
  cell6 = newRow.insertCell(5);
  cell6.innerHTML = data.email;
  cell6 = newRow.insertCell(6);
  cell6.innerHTML = `<a onClick='editarPreventivo(this)'><i class="fa fa-edit" style="font-size: 28px;color:blue"></i></a>`;
  cell7 = newRow.insertCell(7);
  cell7.innerHTML = `<a onClick='borrarPreventivo(this)'><i class="fa fa-trash" style="font-size: 28px;color:red"></i></a>`;
}

function updatePreventivo(formData) {
  if(categoria == 'Preventivo') {
    selectedRow.cells[0].innerHTML = formData.otraActividad;
  }else{
    selectedRow.cells[0].innerHTML = formData.actividad;
  }
  selectedRow.cells[1].innerHTML = formData.amenaza;
  selectedRow.cells[2].innerHTML = formData.preventivo;
  selectedRow.cells[3].innerHTML = formData.fechaInicial;
  if(categoria == 'Preventivo') {
    selectedRow.cells[4].innerHTML = formData.otroTrabajador;
  }else{
    selectedRow.cells[4].innerHTML = formData.trabajador;
  }
  selectedRow.cells[5].innerHTML = formData.email;
}

function editarMitigador(td) {
  resetPreventivo();
  selectedRow = td.parentElement.parentElement;
  document.getElementById("actividades").value = selectedRow.cells[0].innerHTML;
  document.getElementById("otraConsecuencia").value = selectedRow.cells[1].innerHTML;
  document.getElementById("mitigador").value = selectedRow.cells[2].innerHTML;
  document.getElementById("fechaMitigador").value = selectedRow.cells[3].innerHTML;
  document.getElementById("otrosTrabajador").value = selectedRow.cells[4].innerHTML;
  document.getElementById("email").value = selectedRow.cells[5].innerHTML;
  document.getElementById("formMitigador").style.display = "block";
  document.getElementById('controlador2').style.display = 'none'  
}

function borrarMitigador(td) {
  if (confirm('Esta seguro(a) de borrar este control ?')) {
    row = td.parentElement.parentElement;
    document.getElementById("tabla4").deleteRow(row.rowIndex);
    resetPreventivo();
  }
}

function guardarMitigador(cat, id, idtabla) {
  categoria = cat;
  var formData = readMitigador();
  if (selectedRow == null) {
    insertNewMitigador(formData, idtabla);
  }
  else{
    updateMitigador(formData);
  }
  resetMitigador();
  document.getElementById(id).style.display='none';    
}

function readMitigador() {
  var formData = {};
  if(categoria == 'Mitigador') {
    formData["actividades"] = document.getElementById("actividades").value;
  }else {
    formData["otraActividad"] = document.getElementById("otraActividad").value;
  }
  formData["otraConsecuencia"] = document.getElementById("otraConsecuencia").value;
  formData["mitigador"] = document.getElementById("mitigador").value;
  formData["fechaMitigador"] = document.getElementById("fechaMitigador").value;
  if(categoria == 'Mitigador') {
    formData["otrosTrabajador"] = document.getElementById("otrosTrabajador").value;

  }else {
    formData["otroTrabajador"] = document.getElementById("otroTrabajador").value;
  }
  formData["emailMitigador"] = document.getElementById("emailMitigador").value;
  return formData;
}

function insertNewMitigador(data, idtabla) {
  tabla2 = document.getElementById(idtabla).getElementsByTagName('tbody')[0];
  newRow = tabla2.insertRow(tabla2.length);
  if(categoria == 'Mitigador') {
    cell1 = newRow.insertCell(0);
    cell1.innerHTML = data.actividades;
  }else {
    cell1 = newRow.insertCell(0);
    cell1.innerHTML = data.otraActividad;
  }
  cell2 = newRow.insertCell(1);
  cell2.innerHTML = data.otraConsecuencia;
  cell3 = newRow.insertCell(2);
  cell3.innerHTML = data.mitigador;
  cell4 = newRow.insertCell(3);
  cell4.innerHTML = data.fechaMitigador;
  if(categoria == 'Mitigador') {
    cell5 = newRow.insertCell(4);
    cell5.innerHTML = data.otrosTrabajador;
  }else {
    cell5 = newRow.insertCell(4);
    cell5.innerHTML = data.otroTrabajador;
  }
  cell6 = newRow.insertCell(5);
  cell6.innerHTML = data.emailMitigador;
  cell7 = newRow.insertCell(6);
  cell7.innerHTML = `<a onClick='editarMitigador(this)'><i class="fa fa-edit" style="font-size: 28px;color:blue"></i></a>`;
  cell8 = newRow.insertCell(7);
  cell8.innerHTML = `<a onClick='borrarMitigador(this)'><i class="fa fa-trash" style="font-size: 28px;color:red"></i></a>`;
}

function updateMitigador(formData) {
  if(categoria == 'Mitigador') {
    selectedRow.cells[0].innerHTML = formData.actividades;
  }else {
    selectedRow.cells[0].innerHTML = formData.otraActividad;
  }
  selectedRow.cells[1].innerHTML = formData.otraConsecuencia;
  selectedRow.cells[2].innerHTML = formData.mitigador;
  selectedRow.cells[3].innerHTML = formData.fechaMitigador;
  if(categoria == 'Mitigador') {
    selectedRow.cells[4].innerHTML = formData.otrosTrabajador;
  }else {
    selectedRow.cells[4].innerHTML = formData.otroTrabajador;
  }
  selectedRow.cells[5].innerHTML = formData.emailMitigador;
}

function updateAcontecimiento(formData) {
  selectedRow.cells[0].innerHTML = formData.sede;
  selectedRow.cells[1].innerHTML = formData.actividad;
  selectedRow.cells[2].innerHTML = formData.lugar;
  selectedRow.cells[3].innerHTML = formData.trabajador;
  selectedRow.cells[4].innerHTML = formData.descripcion;
  selectedRow.cells[5].innerHTML = formData.clasePeligro;
  selectedRow.cells[6].innerHTML = formData.tipoPeligro;
  selectedRow.cells[7].innerHTML = formData.tiempo;
  selectedRow.cells[8].innerHTML = formData.actoInseguro;
  selectedRow.cells[9].innerHTML = formData.condicionInsegura;
  selectedRow.cells[10].innerHTML = formData.fechaNotifica;
  selectedRow.cells[11].innerHTML = formData.imagen;
  selectedRow.cells[12].innerHTML = formData.consecuencia;
  selectedRow.cells[13].innerHTML = formData.perdida;
  selectedRow.cells[14].innerHTML = formData.personal;
  selectedRow.cells[15].innerHTML = formData.organizacional;
  selectedRow.cells[16].innerHTML = formData.recomendacion;
  selectedRow.cells[17].innerHTML = formData.reporteTrabajador;
}

function formAcontecimiento() {
  var formData = {};
  formData["sede"] = document.getElementById("sede").value;
  formData["actividad"] = document.getElementById("actividad").value;
  formData["lugar"] = document.getElementById("lugar").value;
  formData["trabajador"] = document.getElementById("trabajador").value;
  formData["descripcion"] = document.getElementById("descripcion").value;
  formData["clasePeligro"] = document.getElementById("clasePeligro").value;
  formData["tipoPeligro"] = document.getElementById("tipoPeligro").value;
  formData["tiempo"] = document.getElementById("tiempo").value;
  formData["actoInseguro"] = document.getElementById("actoInseguro").value;
  formData["condicionInsegura"] = document.getElementById("condicionInsegura").value;
  formData["fechaNotifica"] = document.getElementById("fechaNotifica").value;
  formData["imagen"] = document.getElementById("imagen").value;
  formData["consecuencia"] = document.getElementById("consecuencia").value;
  formData["perdida"] = document.querySelector('input[name="perdida"]:checked').value
  formData["personal"] = document.getElementById("personal").value;
  formData["organizacional"] = document.getElementById("organizacional").value;
  formData["recomendacion"] = document.getElementById("recomendacion").value;
  formData["reporteTrabajador"] = document.getElementById("reporteTrabajador").value;
  return formData;
}

function insertNewAcontecimiento(data, idtabla) {
  tabla1 = document.getElementById(idtabla).getElementsByTagName('tbody')[0];
  newRow = tabla1.insertRow(tabla1.length);
  cell1 = newRow.insertCell(0);
  cell1.innerHTML = data.sede;
  cell4 = newRow.insertCell(1);
  cell4.innerHTML = data.actividad;
  cell5 = newRow.insertCell(2);
  cell5.innerHTML = data.lugar;
  cell5 = newRow.insertCell(3);
  cell5.innerHTML = data.trabajador;
  cell5 = newRow.insertCell(4);
  cell5.innerHTML = data.descripcion;
  cell5 = newRow.insertCell(5);
  cell5.innerHTML = data.clasePeligro;
  cell5 = newRow.insertCell(6);
  cell5.innerHTML = data.tipoPeligro;
  cell5 = newRow.insertCell(7);
  cell5.innerHTML = data.tiempo;
  cell5 = newRow.insertCell(8);
  cell5.innerHTML = data.actoInseguro;
  cell5 = newRow.insertCell(9);
  cell5.innerHTML = data.condicionInsegura;
  cell5 = newRow.insertCell(10);
  cell5.innerHTML = data.fechaNotifica;
  cell5 = newRow.insertCell(11);
  cell5.innerHTML = data.imagen;
  cell5 = newRow.insertCell(12);
  cell5.innerHTML = data.consecuencia;
  cell5 = newRow.insertCell(13);
  cell5.innerHTML = data.perdida;
  cell5 = newRow.insertCell(14);
  cell5.innerHTML = data.personal;
  cell5 = newRow.insertCell(15);
  cell5.innerHTML = data.organizacional;
  cell5 = newRow.insertCell(16);
  cell5.innerHTML = data.recomendacion;
  cell5 = newRow.insertCell(17);
  cell5.innerHTML = data.reporteTrabajador;
  cell6 = newRow.insertCell(18);
  cell6.innerHTML = `<a onClick='editarAcontecimiento(this)'><i class="fa fa-edit" style="font-size: 28px;color:blue"></i></a>`;
  cell7 = newRow.insertCell(19);
  cell7.innerHTML = `<a onClick='borrarAcontecimiento(this)'><i class="fa fa-trash" style="font-size: 28px;color:red"></i></a>`;
}

function editarAcontecimiento(td) {
  resetAcontecimiento();
  selectedRow = td.parentElement.parentElement;
  document.getElementById("sede").value = selectedRow.cells[0].innerHTML;
  document.getElementById("actividad").value = selectedRow.cells[1].innerHTML;
  document.getElementById("lugar").value = selectedRow.cells[2].innerHTML;
  document.getElementById("trabajador").value = selectedRow.cells[3].innerHTML;
  document.getElementById("descripcion").value = selectedRow.cells[4].innerHTML;
  document.getElementById("clasePeligro").value = selectedRow.cells[5].innerHTML;
  document.getElementById("tipoPeligro").value = selectedRow.cells[6].innerHTML;
  document.getElementById("tiempo").value = selectedRow.cells[7].innerHTML;
  document.getElementById("actoInseguro").value = selectedRow.cells[8].innerHTML;
  document.getElementById("condicionInsegura").value = selectedRow.cells[9].innerHTML;
  document.getElementById("fechaNotifica").value = selectedRow.cells[10].innerHTML;
  //document.getElementById("imagen").value = selectedRow.cells[11].innerHTML;
  document.getElementById("consecuencia").value = selectedRow.cells[12].innerHTML;
  document.getElementById("perdida").value = selectedRow.cells[13].innerHTML;
  document.getElementById("personal").value = selectedRow.cells[14].innerHTML;
  document.getElementById("organizacional").value = selectedRow.cells[15].innerHTML;
  document.getElementById("recomendacion").value = selectedRow.cells[16].innerHTML;
  document.getElementById("reporteTrabajador").value = selectedRow.cells[17].innerHTML;
  document.getElementById('controlador').style.display = 'none'  
  document.getElementById("formAcontecimiento").style.display = "block";
}

function borrarAcontecimiento(td) {
  if (confirm('Esta seguro(a) de borrar este acontecimiento ?')) {
    row = td.parentElement.parentElement;
    document.getElementById("tabla1").deleteRow(row.rowIndex);
    resetAcontecimiento();
  }
}

function guardarAcontecimiento(id, idtabla) {
  var formData = formAcontecimiento();
  if (selectedRow == null) {
    insertNewAcontecimiento(formData, idtabla);
  }
  else{
    updateAcontecimiento(formData);
  }
  resetAcontecimiento();
  document.getElementById(id).style.display='none';    
}

function updateMitigador(formData) {
  selectedRow.cells[0].innerHTML = formData.otraActividad;
  selectedRow.cells[1].innerHTML = formData.otraConsecuencia;
  selectedRow.cells[2].innerHTML = formData.mitigador;
  selectedRow.cells[3].innerHTML = formData.fechaInicial;
  selectedRow.cells[4].innerHTML = formData.otroTrabajador;
}

function guardarActividad(id, idtabla) {
    if (validate()) {
        var formData = readFormData();
        if (selectedRow == null)
            insertNewRecord(formData, idtabla);
        else
            updateRecord(formData);
    }
    resetForm();
    document.getElementById(id).style.display='none';    
}

function readFormData() {
    var formData = {};
    formData["codigoActividad"] = document.getElementById("codigoActividad").value;
    formData["nombreActividad"] = document.getElementById("nombreActividad").value;
    formData["archivo"] = document.getElementById("archivo").value;
    formData["amenaza"] = document.getElementById("amenaza").value;
    formData["consecuencia"] = document.getElementById("consecuencia").value;
    return formData;
}

function insertNewRecord(data, idtabla) {
    tabla = document.getElementById(idtabla).getElementsByTagName('tbody')[0];
    newRow = tabla.insertRow(tabla.length);
    cell1 = newRow.insertCell(0);
    cell1.innerHTML = data.codigoActividad;
    cell2 = newRow.insertCell(1);
    cell2.innerHTML = data.nombreActividad;
    cell3 = newRow.insertCell(2);
    cell3.innerHTML = data.archivo;
    cell4 = newRow.insertCell(3);
    cell4.innerHTML = data.amenaza;
    cell5 = newRow.insertCell(4);
    cell5.innerHTML = data.consecuencia;
    cell6 = newRow.insertCell(5);
    cell6.innerHTML = `<a onClick='editarActividad(this)'><i class="fa fa-edit" style="font-size: 28px;color:blue"></i></a>`;
    cell7 = newRow.insertCell(6);
    cell7.innerHTML = `<a onClick='borrarActividad(this)'><i class="fa fa-trash" style="font-size: 28px;color:red"></i></a>`;
}

function resetAcontecimiento(td) {
  document.getElementById("sede").value = null;
  document.getElementById("actividad").value = null;
  document.getElementById("lugar").value = null;
  document.getElementById("trabajador").value = null;
  document.getElementById("descripcion").value = null;
  document.getElementById("clasePeligro").value = null;
  document.getElementById("tipoPeligro").value = null;
  document.getElementById("tiempo").value = null;
  document.getElementById("actoInseguro").value = null;
  document.getElementById("condicionInsegura").value = null;
  document.getElementById("fechaNotifica").value = null;
  document.getElementById("imagen").value = null;
  document.getElementById("consecuencia").value = null;
  document.getElementById("personal").value = null;
  document.getElementById("organizacional").value = null;
  document.getElementById("recomendacion").value = null;
  document.getElementById("reporteTrabajador").value = null;
}

function resetPreventivo() {
  document.getElementById("preventivo").value = null;
  document.getElementById("fechaInicial").value = null;
  document.getElementById("trabajador").value = null;
}

function resetMitigador() {
  document.getElementById("mitigador").value = null;
  document.getElementById("fechaInicial").value = null;
  document.getElementById("otroTrabajador").value = null;
}

function resetForm() {
  document.getElementById("amenaza").value = null;
  document.getElementById("consecuencia").value = null;
}

function reset() {
  document.getElementById("codigo").value = null;
  document.getElementById("nombre").value = null;
  document.getElementById("categoria").value = null;
  document.getElementById("valoracion").value = null;
  document.getElementById("finalidad").value = null;
  document.getElementById("trabajador").value = null;
  document.getElementById("archivo").value = null;
  document.getElementById("presupuesto").value = null;
  document.getElementById("observa").value = null;
  document.getElementById("descripcion").value = null;
  document.getElementById("nombre").value = null;
  document.getElementById("fInicial").value = null;
  document.getElementById("fFinal").value = null;
  document.getElementById("efectividad").value = null;
  document.getElementById("observa").value = null;
  document.getElementById("actividad").value = null;
  document.getElementById("amenaza").value = null;
  document.getElementById("control").value = null;
  document.getElementById("fecha").value = null;
  document.getElementById("responsable").value = null;
  document.getElementById("apellido1").value = null;
  document.getElementById("apellido2").value = null;
  document.getElementById("nombres").value = null;
  document.getElementById("documento").value = null;
  document.getElementById("fechaNacimiento").value = null;
  document.getElementById("genero").value = null;
  document.getElementById("estadoCivil").value = null;
  document.getElementById("tipo").value = null;
  document.getElementById("email").value = null;
  document.getElementById("sede").value = null;
  document.getElementById("cargo").value = null;
  document.getElementById("centro").value = null;
  document.getElementById("fechaIngreso").value = null;
  document.getElementById("eps").value = null;
  document.getElementById("afp").value = null;
  document.getElementById("arl").value = null;
  document.getElementById("address").value = null;
  document.getElementById("phone").value = null;
  document.getElementById("riesgo").value = null;
  document.getElementById("actividad").value = null;
}

function validate() {
    isValid = true;
    if (document.getElementById("nombreActividad").value == "") {
        isValid = false;
        document.getElementById("nombreActividadValidationError").classList.remove("hide");
    } else {
        isValid = true;
        if (!document.getElementById("nombreActividadValidationError").classList.contains("hide"))
            document.getElementById("nombreActividadValidationError").classList.add("hide");
    }
    return isValid;
}

function agregarControl(id) {
  document.getElementById(id).style.display = "block";
  document.getElementById(id).style.backgroundColor ="#ddd";
  document.getElementById(id).style.padding = "20px";
  document.getElementById(id).style.width = "110%";
  selectedRow = null
}

function nivelProbabilidad() {
  switch (probabilidad) {
    case (probabilidad >= 24 || probabilidad <= 40):
      document.getElementById("nivelProbabilidad").innerHTML = "Muy alto (MA)"
      break;
    case (probabilidad >= 10 || probabilidad <= 20):
      document.getElementById("nivelProbabilidad").innerHTML = "Alto (A)"
      break;
    case (probabilidad >= 8 || probabilidad <= 6):
      document.getElementById("nivelProbabilidad").innerHTML = "Medio (M)"
      break;
    case (probabilidad >= 2 || probabilidad <= 4):
      document.getElementById("nivelProbabilidad").innerHTML = "Bajo (B)"
      break;
        
    default:
      break;
  }
}

var sede = [
    {Nombre:"Bogota"}, {Nombre:"Medellín"}, {Nombre:"Cali"}, {Nombre:"Peru"}
]

var actoInseguro = [
  {Nombre:"Distracción"}, {Nombre:"Prisa"}, {Nombre:"Cansancio"}, {Nombre:"Uso EPP"},
  {Nombre:"Exceso confianza"}, {Nombre:"Indiferencia"}, {Nombre:"Comportamiento ambiente"}
]

var condicionInsegura = [
  {Nombre:"Orden y limpieza"}, {Nombre:"Superficies defectuosas"}, {Nombre:"Derramaes"}, {Nombre:"Instalaciones electricas"},
  {Nombre:"Instalaciones locativas"}, {Nombre:"Vehiculos"}, {Nombre:"Equipos"},{Nombre:"Sustancias quimicas"},{Nombre:"Otros"}
]

var centro = [
  {Nombre:"Produccion"},{Nombre:"Bodega"},{Nombre:"Gerencia"},{Nombre:"Almacen"}
]

var lugar = [
  {Nombre:"01"}, {Nombre:"02"}, {Nombre:"03"}, {Nombre:"04"}
]

var cargo = [
  {Nombre:"Gerente"}, {Nombre:"Auxiliar bodega"}, {Nombre:"Secretaria Gerencia"}, {Nombre:"Albañil"}
]

var tarea = [
  {Nombre:"Digitador"}, {Nombre:"Enchapado"}, {Nombre:"Laminado"}, {Nombre:"Enladrillar"}, {Nmbre:"Embaldozar"}
]

var actividad = [
  {Nombre:"Mantenimiento andamios"}, {Nombre:"Limpieza paredes y pisos"}, {Nombre:"Trabajo alturas"}, {Nombre:"Embaldosar"}, {Nombre:"Empastar"},
  {Nombre:"Resane y pintura"}, {Nombre:"Adaptar zocalos"}, {Nombre:"Instalar puertas y vetanas"}, {Nombre:"Auxiliar Administrativo 1"}
]

var proceso = [
  {Nombre:"01"}, {Nombre:"02"}, {Nombre:"03"}, {Nombre:"04"}
]

var trabajador = [
  {Nombre:"Francisco Puerta", Documento:71579486}, {Nombre:"David Puerta", Documento:1039460078}, {Nombre:"Patricia Bernal", Documento:43033005}, {Nombre:"Sebastian Puerta", Documento:71579486},
  {Nombre:"Andres Bedoya", Documento:71579486}, {Nombre:"Mateo Roldan", Documento:71579486}, {Nombre:"Maria Jose echeverri", Documento:71579486}, {Nombre:"Daniela soto", Documento:71579486},
  {Nombre:"Benjamin Anzola", Documento:71579486}, {Nombre:"German Gomez", Documento:71579486}, {Nombre:"Martha yepes", Documento:71579486}, {Nombre:"Marta Arboleda", Documento:71579486}
]

var preventivo = [
  {Nombre:"Refuerzo estructural andamio"}, {Nombre:"Ejercicios de relajación"}, {Nombre:"Prueba temperatura"}, {Nombre:"Valoración sicológica"},
  {Nombre:"Refuerzo inducción uso EPP"}, {Nombre:"Lavado de manos frecuente"}, {Nombre:"Toma signos vtales"}, {Nombre:"Valoración médica"}
]

var mitigador = [
  {Nombre:"Refuerzo EPP"}, {Nombre:"Instalar lonas piso"}, {Nombre:"Uso botiquin PA"}, {Nombre:"Valoración sicológica"}
]

var amenaza = [
  {Nombre:"No usa EPP"}, {Nombre:"Se presenta con estado de alcohol"}, {Nombre:"Se presenta con fiebre"}, {Nombre:"Se presenta trasnochado"},
  {Nombre:"Se presenta con tos frecuente"}, {Nombre:"Se presenta con diarrea y vomito"}, {Nombre:"Se presenta con fiebre"}, {Nombre:"Uso de sustancias psicoactivas"}
]

var consecuencia = [
  {Nombre:"Caida con fractura leve"}, {Nombre:"Caida con fractura moderada"}, {Nombre:"Caida con fractura fuerte"}, {Nombre:"Muerte instantanea"},
  {Nombre:"Desmayo"}, {Nombre:"Maltrato a compañeros"}, {Nombre:"Bajo rendimiento"}, {Nombre:"Deterioro de la salud"}
]

var estadosCivil = ["Soltero (a)", "Casado (a)", "Union libre", "Separado (a)", "Divorsiado (a)", "Viudo (a)"];

var efectoPosible = ["Daño Leve", "Daño Medio", "Daño Extremo"];
// var nivelDeficiencia =  ["Muy alto (MA)", "Alto (A)", "Medio (M)", "Bajo (B)"];

var nivelExposicion = [
  {Nombre:"Continua (EC)"},{Nombre:"Frecuente (EF)"},{Nombre:"Ocasional (EO)"},{Nombre:"Esporadica (EE)"}
];

var nivelConsecuencia = [
  {Nombre:"Mortal catastrófico"}, {Nombre:"Muy grave"}, {Nombre:"Grave"}, {Nombre:"Leve"}
]  ;

var TiposVinculacion = ["Nomina", "Prestacion Servicios", "Agremiacion"]

var ND = 0, NE = 0, NP = 0, NC = 0, NR = 0;
var CNR = "";
var CAR = "";
var atColor = "";
var interpreta;
var subjectObject ={};

var nivelDeficiencia =  [
  {Nombre:"Muy alto (MA)"}, {Nombre:"Alto (A)"}, {Nombre:"Medio (M)"}, {Nombre:"Bajo (B)"}
];

function nivelDeficiencias() {
  var select = document.getElementById("nivelDeficiencia");
  var valor = select.options[select.selectedIndex].value;
  switch (valor) {

    case "Muy alto (MA)":
      ND = 10;
      break;
  
    case "Alto (A)":
      ND = 6;
      break;

    case "Medio (M)":
      ND = 2;
      break;

    case "Bajo (B)":
      ND = 0;
      break;

      default:
      break;
  }
  document.getElementById("interpretacionND").innerHTML = ND;
  nivelProbabilidades();
  nivelRiesgos();
  aceptabilidadRiesgos();

}

function nivelExposiciones() {
  var seleccion = document.getElementById("nivelExposicion");
  var valor = seleccion.options[seleccion.selectedIndex].value;
  switch (valor) {

    case "Continua (EC)":
      NE = 4;
      break;
  
    case "Frecuente (EF)":
      NE = 3;
      break;

    case "Ocasional (EO)":
      NE = 2;
      break;

    case "Esporadica (EE)":
      NE = 1;
      break;

      default:
      break;
  }
  document.getElementById("interpretacionNE").innerHTML = NE;
  nivelProbabilidades();
  nivelRiesgos();
  aceptabilidadRiesgos();

}

function nivelRiesgos() {
  var seleccion = document.getElementById("nivelConsecuencia");
  var valor = seleccion.options[seleccion.selectedIndex].value;
  switch (valor) {

    case "Mortal catastrófico":
      NC = 100;
      break;
  
    case "Muy grave":
      NC = 60;
      break;

    case "Grave":
      NC = 25;
      break;

    case "Leve":
      NC = 10;
      break;

      default:
      break;
  }
  NR = NP*NC;
  document.getElementById("interpretacionNC").innerHTML = NC;
  document.getElementById("interpretacionNR").innerHTML = NR;
  nivelProbabilidades();
  aceptabilidadRiesgos();

}

function aceptabilidadRiesgos() {
  if(NR>=600 && NR<=4000){
    CNR = "I";
    CAR = "No Aceptable";
    atColor = "red"
  }

  if(NR>=150 && NR<600){
    CNR = "II";
    CAR = "Aceptable con control Específico";
    atColor = "yellow"
  }

  if(NR>=40 && NR<150){
    CNR = "III";
    CAR = "Mejorable";
    atColor = "orange"
  }

  if(NR<40){
    CNR = "IV";
    CAR = "Aceptable";
    atColor = "green"
  }
  document.getElementById("interpretacionCNR").innerHTML = CNR;
  document.getElementById("interpretacionCAR").innerHTML = CAR;
  document.getElementById("interpretacionCAR").style.backgroundColor = atColor;

}

function nivelProbabilidades() {
  NP = ND*NE;
  document.getElementById("interpretacionNP").innerHTML = NP;
}

function mostrar() {
  document.getElementsByClassName("mostrar")[0].style.display = "inline-block";
}

function cargarDatos(){

    subjectObject = {
      "Físico": {
        "Ruido":[],
        "Iluminación":[],
        "Vibración":[],
        "Temperaturas Extremas":[],
        "Presión atmosferica":[],
        "Radiaciones Ionizantes":[],
        "Radiaciones No Ionozantes":[]
      },
      "Quimico": {
        "Polvos orgánicos, inorgánicos":[],
        "Fibras":[],
        "Liquidos":[],
        "Gases y vapores":[],
        "Humos metálicos, no metálicos":[],
        "Material particulado":[]
      },
      "Biologico": {
        "Virus":[],
        "Bacterias":[],
        "Hongos":[],
        "Ricketsias":[],
        "Parasitos":[],
        "Picaduras":[],
        "Mordeduras":[],
        "Fluidos o escrementos":[]
      },
      "Biomecanicos": {
        "Postura":[],
        "Esfuerzo":[],
        "Movimiento repetitivo":[],
        "Manipulación manual de cargas":[],
      },
      "Electricos": {
        "":[],
        "":[]
      },
      "Fuego Exploción": {
        "":[],
        "":[]
      },
      "Psicosocial": {
        "Gestión organizacional":[],
        "Caracteristicas de la organización del trabajo":[],
        "Caracteristicas del grupo social del trabajo":[],
        "Condiciones de la tarea":[],
        "Interface Persona – tarea":[],
        "Jornada de trabajo":[]
      },
      "Condiciones de seguridad": {
        "Mecanico":[],
        "Electrico":[],
        "Locativo":[],
        "Accidentes de tránsito":[],
        "Publicos":[],
        "Trabajo en alturas":[],
        "Espacios confinados":[]
      },
      "Desastres Naturales": {
        "Sismo":[],
        "Terremoto":[],
        "Vendaval":[],
        "Inundaciones":[],
        "Derrumbes":[],
        "Precipitaciones":[]
      },
  }
  clasePeligros();

  selecContext("centro");
  selecContext("lugar");
  selecContext("cargo");
  selecContext("proceso");
  selecContext("actividad");
  selecContext("tarea");
  selecContext("actoInseguro");
  selecContext("condicionInsegura");
  selecContext("nivelDeficiencia");
  selecContext("nivelExposicion");
  selecContext("nivelConsecuencia");
}

function selecContext(nombre){
    var select = document.getElementById(nombre);
    var nombres = [];
    switch (nombre) {

        case "sede":
            nombres = sede;
            addOption(nombres, select)
            break;
    
        case "centro":
            nombres = centro;
            addOption(nombres, select)
            break;
    
        case "lugar":
            nombres = lugar;
            addOption(nombres, select)
            break;

        case "cargo":
            nombres = cargo;
            addOption(nombres, select)
            break;
    
        case "proceso":
            nombres = proceso;
            addOption(nombres, select)
            break;
    
        case "actoInseguro":
          nombres = actoInseguro;
          addOption(nombres, select)
          break;

        case "actosInseguros":
          nombres = actoInseguro.sort((a,b) => (a.Nombre > b.Nombre) ? 1 : ((b.Nombre > a.Nombre) ? -1 : 0))          
          addOption(nombres, select)
          break;
    
        case "condicionInsegura":
          nombres = condicionInsegura;
          addOption(nombres, select)
          break;

        case "condicionesInseguras":
          nombres = condicionInsegura.sort();
          addOption(nombres, select)
          break;
  
        case "nivelDeficiencia":
          nombres = nivelDeficiencia;
          addOption(nombres, select);
          break;

        case "nivelExposicion":
          nombres = nivelExposicion;
          addOption(nombres, select)
          break;
        case "nivelConsecuencia":
          nombres = nivelConsecuencia;
          addOption(nombres, select)
          break;
  
        case "estadosCivil":
          nombres = estadosCivil;
          addOption(nombres, select)
          break;
    
        case "tarea":
          nombres = tarea;
          addOption(nombres, select)
          break;

        case "actividad":
          nombres = actividad;
          addOption(nombres, select)
          break;

        case "actividades":
          nombres = actividad.sort((a,b) => (a.Nombre > b.Nombre) ? 1 : ((b.Nombre > a.Nombre) ? -1 : 0))          
          addOption(nombres, select)
          break;

        case "otraActividad":
          nombres = actividad.sort((a,b) => (a.Nombre > b.Nombre) ? 1 : ((b.Nombre > a.Nombre) ? -1 : 0))          
          addOption(nombres, select)
          break;
    
        case "amenaza":
          nombres = amenaza;
          addOption(nombres, select)
          break;

        case "otraAmenaza":
            nombres = amenaza;
            addOption(nombres, select)
            break;
  
        case "consecuencia":
          nombres = consecuencia;
          addOption(nombres, select)
          break;

        case "otraConsecuencia":
          nombres = consecuencia;
          addOption(nombres, select)
          break;
  
        case "preventivo":
          nombres = preventivo;
          addOption(nombres, select)
          break;

        case "preventivos":
          nombres = preventivo;
          addOption(nombres, select)
          break;
  
        case "mitigador":
          nombres = mitigador;
          addOption(nombres, select)
          break;

        case "mitigadores":
          nombres = mitigador;
          addOption(nombres, select)
          break;
            
        case "trabajador":
          nombres = trabajador.sort((a,b) => (a.Nombre > b.Nombre) ? 1 : ((b.Nombre > a.Nombre) ? -1 : 0))          
          addNombres(nombres, select)
          break;

        case "reporteTrabajador":
          nombres = trabajador.sort((a,b) => (a.Nombre > b.Nombre) ? 1 : ((b.Nombre > a.Nombre) ? -1 : 0))          
          addNombres(nombres, select)
          break;
            
        case "trabajadores":
          nombres = trabajador.sort((a,b) => (a.Nombre > b.Nombre) ? 1 : ((b.Nombre > a.Nombre) ? -1 : 0))          
          addNombres(nombres, select)
          break;

        case "otroTrabajador":
          nombres = trabajador.sort((a,b) => (a.Nombre > b.Nombre) ? 1 : ((b.Nombre > a.Nombre) ? -1 : 0))          
          addNombres(nombres, select)
          break;

        case "otrosTrabajador":
          nombres = trabajador.sort((a,b) => (a.Nombre > b.Nombre) ? 1 : ((b.Nombre > a.Nombre) ? -1 : 0))          
          addNombres(nombres, select)
        break;
    
            default:
          break;
    }
}

function addOption(nombres, select) {
    for (value in nombres) {
        const option = document.createElement("option");
        option.text = nombres[value].Nombre;
        select.add(option);
    }   
}

function addNombres(nombres, select) {
    for (value in nombres) {
        const option = document.createElement("option");
        option.text = nombres[value].Nombre+" CC: "+nombres[value].Documento;
        select.add(option);
    }   
}

function addDescrip(nombres, select) {
    for (value in nombres) {
        var option = document.createElement("option");
        option.text = nombres[value].Descripcion;
        select.add(option);
    }   
}

function clasePeligros() {
    var subjectSel = document.getElementById("clasePeligro");
    var topicSel = document.getElementById("tipoPeligro");
    for (var x in subjectObject) {
      subjectSel.options[subjectSel.options.length] = new Option(x, x);
    }
    subjectSel.onchange = function() {
      topicSel.length = 1;
      for (var y in subjectObject[this.value]) {
        topicSel.options[topicSel.options.length] = new Option(y, y);
      }
    }
    topicSel.onchange = function() {
    }
}