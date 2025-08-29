var r0 = 0, r1 = 0, r2 = 0, r3 = 0,r4 = 0, r5 = 0, r6 = 0, r7 = 0, r8 = 0,r9 = 0, r10 = 0,
r11 = 0, r12 = 0, r13 = 0, r14 = 0, r15 = 0, r16 = 0, r17 = 0;
var tabla = "", selTodo = false;
var sedeSel = "Todas", centroSel = "Todos", lugarSel = "Todos", procesoSel = "Todos", actividadSel = "Todas", cargoSel = "Todos";
var modelo1 = [];

function seleccion() {
    modelo1 = [];
    sedeSel = document.getElementById("sede").value;
    centroeSel = document.getElementById("centro").value;
    lugarSel = document.getElementById("lugar").value;
    procesoSel = document.getElementById("proceso").value;
    actividadSel = document.getElementById("actividad").value;
    cargoSel = document.getElementById("cargo").value;

    if(sedeSel == "Todas" && centroSel == "Todos" && lugarSel == "Todos" && procesoSel =="Todos"
        && actividadSel == "Todas" && cargoSel == "Todos") { selTodo = true;}

    if(sedeSel == "Todas") { sedeSel = true;}
    if(centroSel == "Todos") { centroSel = true;}
    if(lugarSel == "Todos") { lugarSel = true;}
    if(procesoSel == "Todos") { procesoSel = true;}
    if(actividadSel == "Todas") { actividadSel = true;}
    if(cargoSel == "Todos") { cargoSel = true;}
}

var modelo = [
    {IdentificacionRiesgo:{Sede:"01",Centro:"01",Lugar:"01",Proceso:"01", Actividad:"Aseo general", Tarea: "Limpieza pisos", Descripcion:"Piso resbaladiso",
        Cargo:"Auxiliar bodega", ClasePeligro: "Fisico", TipoPeligro: "Caida", ActoInseguro:"Circulación personal", CondicionInsegura:"Piso mojado", EfectosPosibles:"Discapacida grado 1"
        ,TiempoExposicion:5, FrecuenciaExposicion:8, TrabajadoresExpuestos:15, Eliminacion:"no", Sustitución:"no", ControlIngenieria:
        "no", ControlAdministrativo:"no", Señalizacion:"Colocar mensaje piso", EPP:"Guantes"},
        EvaluacionRiesgo: {NivelProbabilidad: 30, NivelConsecuencias: 100},
        IntervencionRiesgo:{Sustitucion:"Cambio de detergente", ControlesAdministrativos: {
            Nombre: "Secar muy bien los pisos", Responsable: "Juan", Observaciones: "Control administrativo1"}}
        },
    {IdentificacionRiesgo:{Sede:"01",Centro:"01",Lugar:"01",Proceso:"01", Actividad:"Auxiliar Albañil", Tarea: "Pegar ladrillos", Descripcion:"No usa EPP",
    Cargo:"Auxiliar bodega", ClasePeligro: "Fisico", TipoPeligro: "Caida", ActoInseguro:"Circulación personal", CondicionInsegura:"Piso mojado", EfectosPosibles:"Discapacida grado 1"
    , FrecuenciaExposicion:8, TrabajadoresExpuestos:15, Eliminacion:"no", Sustitución:"no", ControlIngenieria:
    "no", ControlAdministrativo:"no", Señalizacion:"Colocar mensaje piso", EPP:"Guantes"},
    EvaluacionRiesgo: {NivelProbabilidad: 40, NivelConsecuencias: 100},
    IntervencionRiesgo:{ControlesIngenieria: {
        Nombre: "Rediseño estructura addamios 1", Responsable: "Pedro", Observaciones: "Control ingenieria 1"}}
        },
    {IdentificacionRiesgo:{Sede:"01",Centro:"01",Lugar:"01",Proceso:"01", Actividad:"Auxiliar Albañil", Tarea: "Embaldozar", Descripcion:"No usa EPP",
    Cargo:"Auxiliar bodega", ClasePeligro: "Fisico", TipoPeligro: "Caida", ActoInseguro:"Circulación personal", CondicionInsegura:"Piso mojado", EfectosPosibles:"Discapacida grado 1"
    , FrecuenciaExposicion:8, TrabajadoresExpuestos:15, Eliminacion:"no", Sustitución:"no", ControlIngenieria:
    "no", ControlAdministrativo:"no", Señalizacion:"Colocar mensaje piso", EPP:"Guantes"},
    EvaluacionRiesgo: {NivelProbabilidad: 20, NivelConsecuencias: 60},
    IntervencionRiesgo:{ControlesIngenieria: {
        Nombre: "Rediseño estructura 2", Responsable: "Pedro", Observaciones: "Control ingenieria 2"}}
        },
    {IdentificacionRiesgo:{Sede:"01",Centro:"01",Lugar:"01",Proceso:"01", Actividad:"Auxiliar Albañil", Tarea: "Enchapar", Descripcion:"No usa EPP",
    Cargo:"Auxiliar bodega", ClasePeligro: "Fisico", TipoPeligro: "Caida", ActoInseguro:"Circulación personal", CondicionInsegura:"Piso mojado", EfectosPosibles:"Discapacida grado 1"
    , FrecuenciaExposicion:8, TrabajadoresExpuestos:15, Eliminacion:"no", Sustitución:"no", ControlIngenieria:
    "no", ControlAdministrativo:"no", Señalizacion:"Colocar mensaje piso", EPP:"Guantes"},
    EvaluacionRiesgo: {NivelProbabilidad: 8, NivelConsecuencias: 60},
    IntervencionRiesgo:{ControlesIngenieria: {
        Nombre: "Rediseño estructura 3", Responsable: "Pedro", Observaciones: "Control ingenieria 3"}}
        },
    {IdentificacionRiesgo:{Sede:"01",Centro:"01",Lugar:"01",Proceso:"01", Actividad:"Auxiliar Albañil", Tarea: "Pintura", Descripcion:"No usa EPP",
    Cargo:"Auxiliar bodega", ClasePeligro: "Biologico", TipoPeligro: "Infección", ActoInseguro:"Circulación personal", CondicionInsegura:"Piso mojado", EfectosPosibles:"Discapacida grado 1"
    , FrecuenciaExposicion:8, TrabajadoresExpuestos:15, Eliminacion:"no", Sustitución:"no", ControlIngenieria:
    "no", ControlAdministrativo:"no", Señalizacion:"Colocar mensaje piso", EPP:"Guantes"},
    EvaluacionRiesgo: {NivelProbabilidad: 4, NivelConsecuencias: 60},
    IntervencionRiesgo:{ControlesIngenieria: {
        Nombre: "Rediseño estructura 3", Responsable: "Pedro", Observaciones: "Control ingenieria 3"}}
        },
    {IdentificacionRiesgo:{Sede:"01",Centro:"01",Lugar:"01",Proceso:"01", Actividad:"Auxiliar Albañil", Tarea: "Entechar", Descripcion:"No usa EPP",
    Cargo:"Auxiliar bodega", ClasePeligro: "Fisico", TipoPeligro: "Caida", ActoInseguro:"Circulación personal", CondicionInsegura:"Piso mojado", EfectosPosibles:"Discapacida grado 1"
    , FrecuenciaExposicion:8, TrabajadoresExpuestos:15, Eliminacion:"no", Sustitución:"no", ControlIngenieria:
    "no", ControlAdministrativo:"no", Señalizacion:"Colocar mensaje piso", EPP:"Guantes"},
    EvaluacionRiesgo: {NivelProbabilidad: 24, NivelConsecuencias: 60},
    IntervencionRiesgo:{ControlesIngenieria: {
        Nombre: "Rediseño estructura 4", Responsable: "Pedro", Observaciones: "Control ingenieria 4"}}
        },
    {IdentificacionRiesgo:{Sede:"01",Centro:"01",Lugar:"01",Proceso:"01", Actividad:"Auxiliar bodega", Tarea: "Cargue materia prima", Descripcion:"No usa EPP",
    Cargo:"Auxiliar bodega", ClasePeligro: "Fisico", TipoPeligro: "Hernia", ActoInseguro:"Circulación personal", CondicionInsegura:"Piso mojado", EfectosPosibles:"Discapacida grado 1"
    , FrecuenciaExposicion:8, TrabajadoresExpuestos:15, Eliminacion:"no", Sustitución:"no", ControlIngenieria:
    "no", ControlAdministrativo:"no", Señalizacion:"Colocar mensaje piso", EPP:"Guantes"},
    EvaluacionRiesgo: {NivelProbabilidad: 30, NivelConsecuencias: 25},
    IntervencionRiesgo:{ControlesIngenieria: {
        Nombre: "Rediseño estructura 5", Responsable: "Pedro", Observaciones: "Control ingenieria 5"}}
        },
    {IdentificacionRiesgo:{Sede:"01",Centro:"01",Lugar:"01",Proceso:"01", Actividad:"Auxiliar Administrativo 1", Tarea: "Digitador", Descripcion:"No usa EPP",
    Cargo:"Auxiliar bodega", ClasePeligro: "Biologico", TipoPeligro: "Contagio", ActoInseguro:"Circulación personal", CondicionInsegura:"Piso mojado", EfectosPosibles:"Discapacida grado 1"
    , FrecuenciaExposicion:8, TrabajadoresExpuestos:15, Eliminacion:"no", Sustitución:"no", ControlIngenieria:
    "no", ControlAdministrativo:"no", Señalizacion:"Colocar mensaje piso", EPP:"Guantes"},
    EvaluacionRiesgo: {NivelProbabilidad: 15, NivelConsecuencias: 10},
    IntervencionRiesgo:{ControlesIngenieria: {
        Nombre: "Rediseño estructura 5", Responsable: "Pedro", Observaciones: "Control ingenieria 5"}}
        },
    {IdentificacionRiesgo:{Sede:"01",Centro:"01",Lugar:"01",Proceso:"01", Actividad:"Auxiliar Administrativo 1", Tarea: "Digitador", Descripcion:"No usa EPP",
    Cargo:"Auxiliar bodega", ClasePeligro: "Biologico", TipoPeligro: "Contagio", ActoInseguro:"Circulación personal", CondicionInsegura:"Piso mojado", EfectosPosibles:"Discapacida grado 1"
    , FrecuenciaExposicion:8, TrabajadoresExpuestos:15, Eliminacion:"no", Sustitución:"no", ControlIngenieria:
    "no", ControlAdministrativo:"no", Señalizacion:"Colocar mensaje piso", EPP:"Guantes"},
    EvaluacionRiesgo: {NivelProbabilidad: 25, NivelConsecuencias: 100},
    IntervencionRiesgo:{ControlesIngenieria: {
        Nombre: "Rediseño estructura 5", Responsable: "Pedro", Observaciones: "Control ingenieria 5"}}
        },
    {IdentificacionRiesgo:{Sede:"01",Centro:"01",Lugar:"01",Proceso:"01", Actividad:"Auxiliar Administrativo 1", Tarea: "Digitador", Descripcion:"No usa EPP",
    Cargo:"Auxiliar bodega", ClasePeligro: "Biologico", TipoPeligro: "Contagio", ActoInseguro:"Circulación personal", CondicionInsegura:"Piso mojado", EfectosPosibles:"Discapacida grado 1"
    , FrecuenciaExposicion:8, TrabajadoresExpuestos:15, Eliminacion:"no", Sustitución:"no", ControlIngenieria:
    "no", ControlAdministrativo:"no", Señalizacion:"Colocar mensaje piso", EPP:"Guantes"},
    EvaluacionRiesgo: {NivelProbabilidad: 35, NivelConsecuencias: 25},
    IntervencionRiesgo:{ControlesIngenieria: {
        Nombre: "Rediseño estructura 5", Responsable: "Pedro", Observaciones: "Control ingenieria 5"}}
        },
    {IdentificacionRiesgo:{Sede:"01",Centro:"01",Lugar:"01",Proceso:"01", Actividad:"Auxiliar Administrativo 1", Tarea: "Digitador", Descripcion:"No usa EPP",
    Cargo:"Auxiliar bodega", ClasePeligro: "Biologico", TipoPeligro: "Contagio", ActoInseguro:"Circulación personal", CondicionInsegura:"Piso mojado", EfectosPosibles:"Discapacida grado 1"
    , FrecuenciaExposicion:8, TrabajadoresExpuestos:15, Eliminacion:"no", Sustitución:"no", ControlIngenieria:
    "no", ControlAdministrativo:"no", Señalizacion:"Colocar mensaje piso", EPP:"Guantes"},
    EvaluacionRiesgo: {NivelProbabilidad: 4, NivelConsecuencias: 10},
    IntervencionRiesgo:{ControlesIngenieria: {
        Nombre: "Rediseño estructura 5", Responsable: "Pedro", Observaciones: "Control ingenieria 5"}}
        },
    {IdentificacionRiesgo:{Sede:"01",Centro:"01",Lugar:"01",Proceso:"01", Actividad:"Auxiliar Administrativo 1", Tarea: "Digitador", Descripcion:"No usa EPP",
    Cargo:"Auxiliar bodega", ClasePeligro: "Biologico", TipoPeligro: "Contagio", ActoInseguro:"Circulación personal", CondicionInsegura:"Piso mojado", EfectosPosibles:"Discapacida grado 1"
    , FrecuenciaExposicion:8, TrabajadoresExpuestos:15, Eliminacion:"no", Sustitución:"no", ControlIngenieria:
    "no", ControlAdministrativo:"no", Señalizacion:"Colocar mensaje piso", EPP:"Guantes"},
    EvaluacionRiesgo: {NivelProbabilidad: 2, NivelConsecuencias: 100},
    IntervencionRiesgo:{ControlesIngenieria: {
        Nombre: "Rediseño estructura 5", Responsable: "Pedro", Observaciones: "Control ingenieria 5"}}
        },
    {IdentificacionRiesgo:{Sede:"01",Centro:"01",Lugar:"01",Proceso:"01", Actividad:"Auxiliar Administrativo 1", Tarea: "Digitador", Descripcion:"No usa EPP",
    Cargo:"Auxiliar bodega", ClasePeligro: "Biologico", TipoPeligro: "Contagio", ActoInseguro:"Circulación personal", CondicionInsegura:"Piso mojado", EfectosPosibles:"Discapacida grado 1"
    , FrecuenciaExposicion:8, TrabajadoresExpuestos:15, Eliminacion:"no", Sustitución:"no", ControlIngenieria:
    "no", ControlAdministrativo:"no", Señalizacion:"Colocar mensaje piso", EPP:"Guantes"},
    EvaluacionRiesgo: {NivelProbabilidad: 6, NivelConsecuencias: 25},
    IntervencionRiesgo:{ControlesIngenieria: {
        Nombre: "Rediseño estructura 5", Responsable: "Pedro", Observaciones: "Control ingenieria 5"}}
        },
    {IdentificacionRiesgo:{Sede:"01",Centro:"01",Lugar:"01",Proceso:"01", Actividad:"Auxiliar Administrativo 1", Tarea: "Digitador", Descripcion:"No usa EPP",
    Cargo:"Auxiliar bodega", ClasePeligro: "Biologico", TipoPeligro: "Contagio"},
    EvaluacionRiesgo: {NivelProbabilidad: 8, NivelConsecuencias: 10},
    IntervencionRiesgo:{ControlesIngenieria: {
        Nombre: "Rediseño estructura 5", Responsable: "Pedro", Observaciones: "Control ingenieria 5"}}
        },
    {IdentificacionRiesgo:{Sede:"01",Centro:"01",Lugar:"01",Proceso:"01", Actividad:"Auxiliar Administrativo 1", Tarea: "Digitador", Descripcion:"No usa EPP",
    Cargo:"Auxiliar bodega", ClasePeligro: "Biologico", TipoPeligro: "Contagio", ActoInseguro:"Circulación personal", CondicionInsegura:"Piso mojado", EfectosPosibles:"Discapacida grado 1"
    , FrecuenciaExposicion:8, TrabajadoresExpuestos:15, Eliminacion:"no", Sustitución:"no", ControlIngenieria:
    "no", ControlAdministrativo:"no", Señalizacion:"Colocar mensaje piso", EPP:"Guantes"},
    EvaluacionRiesgo: {NivelProbabilidad: 17, NivelConsecuencias: 10},
    IntervencionRiesgo:{ControlesIngenieria: {
        Nombre: "Rediseño estructura 5", Responsable: "Pedro", Observaciones: "Control ingenieria 5"}}
        },
    {IdentificacionRiesgo:{Sede:"01",Centro:"01",Lugar:"01",Proceso:"01", Actividad:"Auxiliar Administrativo 1", Tarea: "Digitador", Descripcion:"No usa EPP",
    Cargo:"Auxiliar bodega", ClasePeligro: "Biologico", TipoPeligro: "Contagio", ActoInseguro:"Circulación personal", CondicionInsegura:"Piso mojado", EfectosPosibles:"Discapacida grado 1"
    , FrecuenciaExposicion:8, TrabajadoresExpuestos:15, Eliminacion:"no", Sustitución:"no", ControlIngenieria:
    "no", ControlAdministrativo:"no", Señalizacion:"Colocar mensaje piso", EPP:"Guantes"},
    EvaluacionRiesgo: {NivelProbabilidad: 18, NivelConsecuencias: 100},
    IntervencionRiesgo:{ControlesIngenieria: {
        Nombre: "Rediseño estructura 5", Responsable: "Pedro", Observaciones: "Control ingenieria 5"}}
        },
    {IdentificacionRiesgo:{Sede:"01",Centro:"01",Lugar:"01",Proceso:"01", Actividad:"Auxiliar Administrativo 1", Tarea: "Digitador", Descripcion:"No usa EPP",
    Cargo:"Auxiliar bodega", ClasePeligro: "Biologico", TipoPeligro: "Contagio", ActoInseguro:"Circulación personal", CondicionInsegura:"Piso mojado", EfectosPosibles:"Discapacida grado 1"
    , FrecuenciaExposicion:8, TrabajadoresExpuestos:15, Eliminacion:"no", Sustitución:"no", ControlIngenieria:
    "no", ControlAdministrativo:"no", Señalizacion:"Colocar mensaje piso", EPP:"Guantes"},
    EvaluacionRiesgo: {NivelProbabilidad: 20, NivelConsecuencias: 10},
    IntervencionRiesgo:{ControlesIngenieria: {
        Nombre: "Rediseño estructura 5", Responsable: "Pedro", Observaciones: "Control ingenieria 5"}}
        },
    {IdentificacionRiesgo:{Sede:"01",Centro:"01",Lugar:"01",Proceso:"01", Actividad:"Auxiliar Administrativo 1", Tarea: "Digitador", Descripcion:"No usa EPP",
    Cargo:"Auxiliar bodega", ClasePeligro: "Biologico", TipoPeligro: "Contagio"},
    EvaluacionRiesgo: {NivelProbabilidad: 11, NivelConsecuencias: 60},
    IntervencionRiesgo:{ControlesIngenieria: {
        Nombre: "Rediseño estructura 5", Responsable: "Pedro", Observaciones: "Control ingenieria 5"}}
        },
    {IdentificacionRiesgo:{Sede:"01",Centro:"01",Lugar:"01",Proceso:"01", Actividad:"Auxiliar Administrativo 1", Tarea: "Digitador", Descripcion:"No usa EPP",
    Cargo:"Auxiliar bodega", ClasePeligro: "Biologico", TipoPeligro: "Contagio", ActoInseguro:"Circulación personal", CondicionInsegura:"Piso mojado", EfectosPosibles:"Discapacida grado 1"
    , FrecuenciaExposicion:8, TrabajadoresExpuestos:15, Eliminacion:"no", Sustitución:"no", ControlIngenieria:
    "no", ControlAdministrativo:"no", Señalizacion:"Colocar mensaje piso", EPP:"Guantes"},
    EvaluacionRiesgo: {NivelProbabilidad: 13, NivelConsecuencias: 10},
    IntervencionRiesgo:{ControlesIngenieria: {
        Nombre: "Rediseño estructura 5", Responsable: "Pedro", Observaciones: "Control ingenieria 5"}}
    }
];

function ver0() {
    seleccion();
    if(selTodo){
        modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 24 && m.EvaluacionRiesgo.NivelProbabilidad <= 40 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 100);
    }else{
        modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 24 && m.EvaluacionRiesgo.NivelProbabilidad <= 40 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 100 && m.IdentificacionRiesgo.Sede == sedeSel && m.IdentificacionRiesgo.Centro == centroSel
            && m.IdentificacionRiesgo.Lugar == lugarSel && m.IdentificacionRiesgo.Proceso == procesoSel
            && m.IdentificacionRiesgo.Actividad == actividadSel && m.IdentificacionRiesgo.Cargo == cargoSel);
    }
    tablaRiesgos(modelo1);
}

function ver1() {
    seleccion();
    if(selTodo){
        modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 10 && m.EvaluacionRiesgo.NivelProbabilidad <= 20 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 100);
    } else{
        modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 10 && m.EvaluacionRiesgo.NivelProbabilidad <= 20 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 100 && m.IdentificacionRiesgo.Sede == sedeSel && m.IdentificacionRiesgo.Centro == centroSel
            && m.IdentificacionRiesgo.Lugar == lugarSel && m.IdentificacionRiesgo.Proceso == procesoSel
            && m.IdentificacionRiesgo.Actividad == actividadSel && m.IdentificacionRiesgo.Cargo == cargoSel);
    }
    tablaRiesgos(modelo1);
}

function ver2() {
    seleccion();
    if(selTodo){
        modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 6 && m.EvaluacionRiesgo.NivelProbabilidad <= 8 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 100);
    } else {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 6 && m.EvaluacionRiesgo.NivelProbabilidad <= 8 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 100 && m.IdentificacionRiesgo.Sede == sedeSel && m.IdentificacionRiesgo.Centro == centroSel
            && m.IdentificacionRiesgo.Lugar == lugarSel && m.IdentificacionRiesgo.Proceso == procesoSel
            && m.IdentificacionRiesgo.Actividad == actividadSel && m.IdentificacionRiesgo.Cargo == cargoSel);
    
    }
    tablaRiesgos(modelo1);

}

function ver3() {
    seleccion();
    if (selTodo) {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 2 && m.EvaluacionRiesgo.NivelProbabilidad <= 4 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 100);        
    } else {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 2 && m.EvaluacionRiesgo.NivelProbabilidad <= 4 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 100 && m.IdentificacionRiesgo.Sede == sedeSel && m.IdentificacionRiesgo.Centro == centroSel
            && m.IdentificacionRiesgo.Lugar == lugarSel && m.IdentificacionRiesgo.Proceso == procesoSel
            && m.IdentificacionRiesgo.Actividad == actividadSel && m.IdentificacionRiesgo.Cargo == cargoSel);
    }
    tablaRiesgos(modelo1);

}

function ver4() {
    seleccion();
    if (selTodo) {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 24 && m.EvaluacionRiesgo.NivelProbabilidad <= 40 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 60);        
    } else {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 24 && m.EvaluacionRiesgo.NivelProbabilidad <= 40 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 60 && m.IdentificacionRiesgo.Sede == sedeSel && m.IdentificacionRiesgo.Centro == centroSel
            && m.IdentificacionRiesgo.Lugar == lugarSel && m.IdentificacionRiesgo.Proceso == procesoSel
            && m.IdentificacionRiesgo.Actividad == actividadSel && m.IdentificacionRiesgo.Cargo == cargoSel);
    }
    tablaRiesgos(modelo1);

}

function ver5() {
    seleccion();
    if (selTodo) {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 10 && m.EvaluacionRiesgo.NivelProbabilidad <= 20 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 60);        
    } else {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 10 && m.EvaluacionRiesgo.NivelProbabilidad <= 20 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 60 && m.IdentificacionRiesgo.Sede == sedeSel && m.IdentificacionRiesgo.Centro == centroSel
            && m.IdentificacionRiesgo.Lugar == lugarSel && m.IdentificacionRiesgo.Proceso == procesoSel
            && m.IdentificacionRiesgo.Actividad == actividadSel && m.IdentificacionRiesgo.Cargo == cargoSel);
    }
    tablaRiesgos(modelo1);

}

function ver6() {
    seleccion();
    if (selTodo) {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 6 && m.EvaluacionRiesgo.NivelProbabilidad <= 8 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 60);        
    } else {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 6 && m.EvaluacionRiesgo.NivelProbabilidad <= 8 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 60 && m.IdentificacionRiesgo.Sede == sedeSel && m.IdentificacionRiesgo.Centro == centroSel
            && m.IdentificacionRiesgo.Lugar == lugarSel && m.IdentificacionRiesgo.Proceso == procesoSel
            && m.IdentificacionRiesgo.Actividad == actividadSel && m.IdentificacionRiesgo.Cargo == cargoSel);
    }
    tablaRiesgos(modelo1);

}

function ver7() {
    seleccion();
    if (selTodo) {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 2 && m.EvaluacionRiesgo.NivelProbabilidad <= 4 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 60);        
    } else {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 2 && m.EvaluacionRiesgo.NivelProbabilidad <= 4 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 60 && m.IdentificacionRiesgo.Sede == sedeSel && m.IdentificacionRiesgo.Centro == centroSel
            && m.IdentificacionRiesgo.Lugar == lugarSel && m.IdentificacionRiesgo.Proceso == procesoSel
            && m.IdentificacionRiesgo.Actividad == actividadSel && m.IdentificacionRiesgo.Cargo == cargoSel);
    }
    tablaRiesgos(modelo1);

}

function ver8() {
    seleccion();
    if (selTodo) {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 24 && m.EvaluacionRiesgo.NivelProbabilidad <= 40 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 25);        
    } else {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 24 && m.EvaluacionRiesgo.NivelProbabilidad <= 40 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 25 && m.IdentificacionRiesgo.Sede == sedeSel && m.IdentificacionRiesgo.Centro == centroSel
            && m.IdentificacionRiesgo.Lugar == lugarSel && m.IdentificacionRiesgo.Proceso == procesoSel
            && m.IdentificacionRiesgo.Actividad == actividadSel && m.IdentificacionRiesgo.Cargo == cargoSel);
    }
    tablaRiesgos(modelo1);

}

function ver9() {
    seleccion();
    if (selTodo) {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 10 && m.EvaluacionRiesgo.NivelProbabilidad <= 20 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 25);        
    } else {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 10 && m.EvaluacionRiesgo.NivelProbabilidad <= 20 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 25 && m.IdentificacionRiesgo.Sede == sedeSel && m.IdentificacionRiesgo.Centro == centroSel
            && m.IdentificacionRiesgo.Lugar == lugarSel && m.IdentificacionRiesgo.Proceso == procesoSel
            && m.IdentificacionRiesgo.Actividad == actividadSel && m.IdentificacionRiesgo.Cargo == cargoSel);
    }
    tablaRiesgos(modelo1);

}

function ver10() {
    seleccion();
    if (selTodo) {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 6 && m.EvaluacionRiesgo.NivelProbabilidad <= 8 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 25);        
    } else {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 6 && m.EvaluacionRiesgo.NivelProbabilidad <= 8 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 25 && m.IdentificacionRiesgo.Sede == sedeSel && m.IdentificacionRiesgo.Centro == centroSel
            && m.IdentificacionRiesgo.Lugar == lugarSel && m.IdentificacionRiesgo.Proceso == procesoSel
            && m.IdentificacionRiesgo.Actividad == actividadSel && m.IdentificacionRiesgo.Cargo == cargoSel);
    }
    tablaRiesgos(modelo1);

}

function ver11() {
    seleccion();
    if (selTodo) {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 2 && m.EvaluacionRiesgo.NivelProbabilidad <= 4 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 25);        
    } else {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 2 && m.EvaluacionRiesgo.NivelProbabilidad <= 4 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 25 && m.IdentificacionRiesgo.Sede == sedeSel && m.IdentificacionRiesgo.Centro == centroSel
            && m.IdentificacionRiesgo.Lugar == lugarSel && m.IdentificacionRiesgo.Proceso == procesoSel
            && m.IdentificacionRiesgo.Actividad == actividadSel && m.IdentificacionRiesgo.Cargo == cargoSel);
    }
    tablaRiesgos(modelo1);

}

function ver12() {
    seleccion();
    if (selTodo) {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 24 && m.EvaluacionRiesgo.NivelProbabilidad <= 40 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 10);        
    } else {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 24 && m.EvaluacionRiesgo.NivelProbabilidad <= 40 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 10 && m.IdentificacionRiesgo.Sede == sedeSel && m.IdentificacionRiesgo.Centro == centroSel
            && m.IdentificacionRiesgo.Lugar == lugarSel && m.IdentificacionRiesgo.Proceso == procesoSel
            && m.IdentificacionRiesgo.Actividad == actividadSel && m.IdentificacionRiesgo.Cargo == cargoSel);
    }
    tablaRiesgos(modelo1);

}

function ver13() {
    seleccion();
    if (selTodo) {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 10 && m.EvaluacionRiesgo.NivelProbabilidad <= 20 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 10);        
    } else {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 10 && m.EvaluacionRiesgo.NivelProbabilidad <= 20 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 10 && m.IdentificacionRiesgo.Sede == sedeSel && m.IdentificacionRiesgo.Centro == centroSel
            && m.IdentificacionRiesgo.Lugar == lugarSel && m.IdentificacionRiesgo.Proceso == procesoSel
            && m.IdentificacionRiesgo.Actividad == actividadSel && m.IdentificacionRiesgo.Cargo == cargoSel);
    }
    tablaRiesgos(modelo1);

}

function ver14() {
    seleccion();
    if (selTodo) {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 6 && m.EvaluacionRiesgo.NivelProbabilidad <= 8 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 10);        
    } else {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 6 && m.EvaluacionRiesgo.NivelProbabilidad <= 8 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 10 && m.IdentificacionRiesgo.Sede == sedeSel && m.IdentificacionRiesgo.Centro == centroSel
            && m.IdentificacionRiesgo.Lugar == lugarSel && m.IdentificacionRiesgo.Proceso == procesoSel
            && m.IdentificacionRiesgo.Actividad == actividadSel && m.IdentificacionRiesgo.Cargo == cargoSel);
    }
    tablaRiesgos(modelo1);

}

function ver15() {
    seleccion();
    if (selTodo) {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 2 && m.EvaluacionRiesgo.NivelProbabilidad <= 4 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 10);        
    } else {
        var modelo1 = modelo.filter(m => m.EvaluacionRiesgo.NivelProbabilidad >= 2 && m.EvaluacionRiesgo.NivelProbabilidad <= 4 
            &&  m.EvaluacionRiesgo.NivelConsecuencias == 10 && m.IdentificacionRiesgo.Sede == sedeSel && m.IdentificacionRiesgo.Centro == centroSel
            && m.IdentificacionRiesgo.Lugar == lugarSel && m.IdentificacionRiesgo.Proceso == procesoSel
            && m.IdentificacionRiesgo.Actividad == actividadSel && m.IdentificacionRiesgo.Cargo == cargoSel);
    }
    tablaRiesgos(modelo1);

}

function tablaRiesgos(modelo1) {
    tabla = document.getElementById("tablaR").getElementsByTagName('tbody')[0];
    tabla.length = 1;
    var hilera = "";
    var celda = "";
    var textoCelda = "";
    for (var i = 0; i < modelo1.length; i++) {

        // Columna 1
        hilera = document.createElement("tr");
        celda = document.createElement("td");
        textoCelda = document.createTextNode(modelo1[i].IdentificacionRiesgo.Sede );
        celda.appendChild(textoCelda);
        hilera.appendChild(celda);
        tabla.appendChild(hilera);

        // Columna 4
        celda = document.createElement("td");
        textoCelda = document.createTextNode(modelo1[i].IdentificacionRiesgo.Proceso );
        celda.appendChild(textoCelda);
        hilera.appendChild(celda);
        tabla.appendChild(hilera);

        // Columna 5
        celda = document.createElement("td");
        textoCelda = document.createTextNode(modelo1[i].IdentificacionRiesgo.Actividad );
        celda.appendChild(textoCelda);
        hilera.appendChild(celda);
        tabla.appendChild(hilera);
        
        // Columna 7
        celda = document.createElement("td");
        textoCelda = document.createTextNode(modelo1[i].IdentificacionRiesgo.Descripcion);
        celda.appendChild(textoCelda);
        hilera.appendChild(celda);
        tabla.appendChild(hilera);
        
        // Columna 8
        celda = document.createElement("td");
        textoCelda = document.createTextNode(modelo1[i].IdentificacionRiesgo.ClasePeligro);
        celda.appendChild(textoCelda);
        hilera.appendChild(celda);
        tabla.appendChild(hilera);
        
        // Columna 9
        celda = document.createElement("td");
        textoCelda = document.createTextNode(modelo1[i].IdentificacionRiesgo.TipoPeligro);
        celda.appendChild(textoCelda);
        hilera.appendChild(celda);
        tabla.appendChild(hilera);
        
        
        // Columna 10
        celda = document.createElement("td");
        textoCelda = document.createTextNode(modelo1[i].IdentificacionRiesgo.ActoInseguro);
        celda.appendChild(textoCelda);
        hilera.appendChild(celda);
        tabla.appendChild(hilera);
        
        // Columna 11
        celda = document.createElement("td");
        textoCelda = document.createTextNode(modelo1[i].IdentificacionRiesgo.CondicionInsegura);
        celda.appendChild(textoCelda);
        hilera.appendChild(celda);
        tabla.appendChild(hilera);
        
        // Columna 12
        celda = document.createElement("td");
        textoCelda = document.createTextNode(modelo1[i].IdentificacionRiesgo.EfectosPosibles);
        celda.appendChild(textoCelda);
        hilera.appendChild(celda);
        tabla.appendChild(hilera);
        
        // Columna 13
        celda = document.createElement("td");
        textoCelda = document.createTextNode(modelo1[i].IdentificacionRiesgo.TiempoExposicion);
        celda.appendChild(textoCelda);
        hilera.appendChild(celda);
        tabla.appendChild(hilera);
        
        // Columna 14
        celda = document.createElement("td");
        textoCelda = document.createTextNode(modelo1[i].IdentificacionRiesgo.FrecuenciaExposicion);
        celda.appendChild(textoCelda);
        hilera.appendChild(celda);
        tabla.appendChild(hilera);
        
        // Columna 15
        celda = document.createElement("td");
        textoCelda = document.createTextNode(modelo1[i].IdentificacionRiesgo.TrabajadoresExpuestos);
        celda.appendChild(textoCelda);
        hilera.appendChild(celda);
        tabla.appendChild(hilera);
        
        // Columna 18
        celda = document.createElement("td");
        textoCelda = document.createTextNode(modelo1[i].IdentificacionRiesgo.ControlPreventivo);
        celda.appendChild(textoCelda);
        hilera.appendChild(celda);
        tabla.appendChild(hilera);
        
        // Columna 18
        celda = document.createElement("td");
        textoCelda = document.createTextNode(modelo1[i].IdentificacionRiesgo.ControlMitigador);
        celda.appendChild(textoCelda);
        hilera.appendChild(celda);
        tabla.appendChild(hilera);

        // Columna 22
        celda = document.createElement("td");
        celda1 = document.createElement("Button");
        celda1.innerHTML = `<a onClick='editarAcontecimiento(this)'><i class="fa fa-edit" style="font-size: 28px;color:blue"></i></a>`;
        celda1.innerHTML = "<i class='fa fa-edit'></i>";
        celda1.addEventListener("click", function() {
            document.getElementsByClassName("preventivo")[0].style.display = "inline-block";
            document.getElementById("clasePeligro").value = modelo1[i].IdentificacionRiesgo.ClasePeligro});
        celda1.setAttribute("style", "background-color: rgb(128, 184, 25); color: white; cursor: pointer; width: 100%; margin-rigth: 0px;");
        celda.appendChild(celda1);
        hilera.appendChild(celda);
        tabla.appendChild(hilera);

        // Columna 23
        celda = document.createElement("td");
        celda1 = document.createElement("Button");
        celda1.innerHTML = "<i class='fa fa-list'></i>";
        celda1.addEventListener("click", function() {
            document.getElementsByClassName("mitigador")[0].style.display = "inline-block";
            document.getElementById("clasePeligro").value = modelo1[i].IdentificacionRiesgo.ClasePeligro;});
        celda1.setAttribute("style", "background-color: red; color: white; cursor: pointer; width: 100%; margin: 0px;");
        celda.appendChild(celda1);
        hilera.appendChild(celda);
        tabla.appendChild(hilera);
    }
}

function consultarTrazabilidad(id) {
    agregarControl(id);
    var modelo = [
        {Codigo:"con01", Nombre:"control actividad 01", Categoria:"Preventivo", Finalidad:"Finalidad amenaza 01", Efectividad:10},
        {Codigo:"con02", Nombre:"control actividad 02", Categoria:"Preventivo", Finalidad:"Finalidad amenaza 02", Efectividad:20},
        {Codigo:"con03", Nombre:"control actividad 03", Categoria:"Preventivo", Finalidad:"Finalidad amenaza 03", Efectividad:30},
        {Codigo:"con04", Nombre:"control actividad 04", Categoria:"Preventivo", Finalidad:"Finalidad amenaza 04", Efectividad:40},
        {Codigo:"con05", Nombre:"control actividad 05", Categoria:"Preventivo", Finalidad:"Finalidad amenaza 05", Efectividad:50},
        {Codigo:"con06", Nombre:"control actividad 06", Categoria:"Preventivo", Finalidad:"Finalidad amenaza 06", Efectividad:60},
        {Codigo:"con07", Nombre:"control actividad 07", Categoria:"Preventivo", Finalidad:"Finalidad amenaza 07", Efectividad:70},
        {Codigo:"con08", Nombre:"control actividad 08", Categoria:"Preventivo", Finalidad:"Finalidad amenaza 08", Efectividad:80},
        {Codigo:"con09", Nombre:"control actividad 09", Categoria:"Preventivo", Finalidad:"Finalidad amenaza 09", Efectividad:90},
        {Codigo:"con01", Nombre:"control actividad 09", Categoria:"Preventivo", Finalidad:"Finalidad amenaza 01", Efectividad:99},
        {Codigo:"con01", Nombre:"control actividad 01", Categoria:"Mitigador", Finalidad:"Finalidad amenaza 01", Efectividad:10},
        {Codigo:"con02", Nombre:"control actividad 02", Categoria:"Mitigador", Finalidad:"Finalidad amenaza 02", Efectividad:20},
        {Codigo:"con03", Nombre:"control actividad 03", Categoria:"Mitigador", Finalidad:"Finalidad amenaza 03", Efectividad:30},
        {Codigo:"con04", Nombre:"control actividad 04", Categoria:"Mitigador", Finalidad:"Finalidad amenaza 04", Efectividad:40},
        {Codigo:"con05", Nombre:"control actividad 05", Categoria:"Mitigador", Finalidad:"Finalidad amenaza 05", Efectividad:50},
        {Codigo:"con06", Nombre:"control actividad 06", Categoria:"Mitigador", Finalidad:"Finalidad amenaza 06", Efectividad:60},
        {Codigo:"con07", Nombre:"control actividad 07", Categoria:"Mitigador", Finalidad:"Finalidad amenaza 07", Efectividad:70},
        {Codigo:"con08", Nombre:"control actividad 08", Categoria:"Mitigador", Finalidad:"Finalidad amenaza 08", Efectividad:80},
        {Codigo:"con09", Nombre:"control actividad 09", Categoria:"Mitigador", Finalidad:"Finalidad amenaza 09", Efectividad:90},
        {Codigo:"con01", Nombre:"control actividad 09", Categoria:"Mitigador", Finalidad:"Finalidad amenaza 01", Efectividad:99}
    ];
    tabla = document.getElementById("tablaC");
    tabla.length = 1;
    var hilera = "";
    var celda = "";
    var textoCelda = "";
    for (var i = 0; i < modelo.length; i++) {

        // Columna 1
        hilera = document.createElement("tr");
        celda = document.createElement("td");
        textoCelda = document.createTextNode(modelo[i].Codigo);
        celda.appendChild(textoCelda);
        hilera.appendChild(celda);
        tabla.appendChild(hilera);

        // Columna 2
        celda = document.createElement("td");
        textoCelda = document.createTextNode(modelo[i].Nombre);
        celda.appendChild(textoCelda);
        hilera.appendChild(celda);
        tabla.appendChild(hilera);
        
        // Columna 3
        celda = document.createElement("td");
        textoCelda = document.createTextNode(modelo[i].Categoria);
        celda.appendChild(textoCelda);
        hilera.appendChild(celda);
        tabla.appendChild(hilera);
        
        // Columna 4
        celda = document.createElement("td");
        textoCelda = document.createTextNode(modelo[i].Finalidad);
        celda.appendChild(textoCelda);
        hilera.appendChild(celda);
        tabla.appendChild(hilera);

        // Columna 5
        celda = document.createElement("td");
        celda1 = document.createElement("Button");
        celda1.innerHTML = "<i class='fa fa-list'></i>";
        celda1.addEventListener("click", function() {

            if (modelo[i].Efectividad >= 80) {
                celda1.setAttribute("style", "background-color:  rgb(128, 184, 25); color: white; padding:10px; width: 100%; margin: 0px;");
            } else {
               if (modelo[i].Efectividad >= 60) {
                celda1.setAttribute("style", "background-color: yellow; color: white; cursor: pointer; width: 100%; margin: 0px;");
               } else {
                celda1.setAttribute("style", "background-color: red; color: white; cursor: pointer; width: 100%; margin: 0px;");
               }
            }
        }); 
        celda.appendChild(celda1);
        hilera.appendChild(celda);
        tabla.appendChild(hilera);
    }
}

function guardar() {
    document.getElementsByClassName("preventiva")[0].style.display = "none";
    document.getElementsByClassName("correctiva")[0].style.display = "none";
}

function preventiva(){
    document.getElementsByClassName("preventiva")[0].style.display = "inline-block";
}

function mapaRiesgos() {
    modelo.forEach(numeroRiesgos);

    function numeroRiesgos(item, index) {
        if (item.EvaluacionRiesgo.NivelProbabilidad >= 24 && item.EvaluacionRiesgo.NivelProbabilidad <= 40 
            &&  item.EvaluacionRiesgo.NivelConsecuencias == 100) {
            r0++}

        if (item.EvaluacionRiesgo.NivelProbabilidad >= 10 && item.EvaluacionRiesgo.NivelProbabilidad <= 20 
            &&  item.EvaluacionRiesgo.NivelConsecuencias == 100) {
            r1++}

        if (item.EvaluacionRiesgo.NivelProbabilidad >= 6 && item.EvaluacionRiesgo.NivelProbabilidad <= 8 
            &&  item.EvaluacionRiesgo.NivelConsecuencias == 100) {
            r2++}

        if (item.EvaluacionRiesgo.NivelProbabilidad >= 2 && item.EvaluacionRiesgo.NivelProbabilidad <= 4 
            &&  item.EvaluacionRiesgo.NivelConsecuencias == 100) {
            r3++}

        if (item.EvaluacionRiesgo.NivelProbabilidad >= 24 && item.EvaluacionRiesgo.NivelProbabilidad <= 40 
            &&  item.EvaluacionRiesgo.NivelConsecuencias == 60) {
            r4++}

        if (item.EvaluacionRiesgo.NivelProbabilidad >= 10 && item.EvaluacionRiesgo.NivelProbabilidad <= 20 
            &&  item.EvaluacionRiesgo.NivelConsecuencias == 60) {
            r5++}

        if (item.EvaluacionRiesgo.NivelProbabilidad >= 6 && item.EvaluacionRiesgo.NivelProbabilidad <= 8 
            &&  item.EvaluacionRiesgo.NivelConsecuencias == 60) {
            r6++}

        if (item.EvaluacionRiesgo.NivelProbabilidad >= 2 && item.EvaluacionRiesgo.NivelProbabilidad <= 4 
            &&  item.EvaluacionRiesgo.NivelConsecuencias == 60) {
            r7++}
        
        if (item.EvaluacionRiesgo.NivelProbabilidad >= 24 && item.EvaluacionRiesgo.NivelProbabilidad <= 40 
            &&  item.EvaluacionRiesgo.NivelConsecuencias == 25) {
            r8++}

        if (item.EvaluacionRiesgo.NivelProbabilidad >= 10 && item.EvaluacionRiesgo.NivelProbabilidad <= 20 
            &&  item.EvaluacionRiesgo.NivelConsecuencias == 25) {
            r9++}

        if (item.EvaluacionRiesgo.NivelProbabilidad >= 6 && item.EvaluacionRiesgo.NivelProbabilidad <= 8 
            &&  item.EvaluacionRiesgo.NivelConsecuencias == 25) {
            r10++}

        if (item.EvaluacionRiesgo.NivelProbabilidad >= 2 && item.EvaluacionRiesgo.NivelProbabilidad <= 4 
            &&  item.EvaluacionRiesgo.NivelConsecuencias == 25) {
            r11++}
    
        if (item.EvaluacionRiesgo.NivelProbabilidad >= 24 && item.EvaluacionRiesgo.NivelProbabilidad <= 40 
            &&  item.EvaluacionRiesgo.NivelConsecuencias == 10) {
            r12++}

        if (item.EvaluacionRiesgo.NivelProbabilidad >= 10 && item.EvaluacionRiesgo.NivelProbabilidad <= 20 
            &&  item.EvaluacionRiesgo.NivelConsecuencias == 10) {
            r13++}

        if (item.EvaluacionRiesgo.NivelProbabilidad >= 6 && item.EvaluacionRiesgo.NivelProbabilidad <= 8 
            &&  item.EvaluacionRiesgo.NivelConsecuencias == 10) {
            r14++}

        if (item.EvaluacionRiesgo.NivelProbabilidad >= 2 && item.EvaluacionRiesgo.NivelProbabilidad <= 4 
            &&  item.EvaluacionRiesgo.NivelConsecuencias == 10) {
            r15++}
    }

    document.getElementById("r0").innerHTML = r0; 
    document.getElementById("r1").innerHTML = r1; 
    document.getElementById("r2").innerHTML = r2; 
    document.getElementById("r3").innerHTML = r3; 
    document.getElementById("r4").innerHTML = r4; 
    document.getElementById("r5").innerHTML = r5; 
    document.getElementById("r6").innerHTML = r6; 
    document.getElementById("r7").innerHTML = r7; 
    document.getElementById("r8").innerHTML = r8; 
    document.getElementById("r9").innerHTML = r9; 
    document.getElementById("r10").innerHTML = r10; 
    document.getElementById("r11").innerHTML = r11; 
    document.getElementById("r12").innerHTML = r12; 
    document.getElementById("r13").innerHTML = r13; 
    document.getElementById("r14").innerHTML = r14; 
    document.getElementById("r15").innerHTML = r15; 
}