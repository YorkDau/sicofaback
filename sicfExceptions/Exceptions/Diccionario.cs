namespace sicfExceptions.Exceptions
{
    public class Diccionario
    {
        //EL # DEL MENSAJE ES UN NUMERO DE 4 DIGITOS 
        //LOS 2 PRIMEROS SON EL # DE LA HISTORIA DE USUARIO 
        //LOS 2 ULTIMOS SON EL # DEL MESAJE 01,02,03
        //EJEMPLO : HU026 LOGIN = # : 2601,2602...
        public Dictionary<int, object> diccionarioES()
        {
            return new Dictionary<int, object>()
           {
                {1000, "Error en Base de Datos"},
                {2001, "Ya existe una deduccion con ese nombre, Por favor, escriba otro." },
                {2002, "Ya existe ese usuario en la base de datos" },
                #region login
                {2601, "Acceso inválido. Por favor, verifique credenciales"},
                #endregion
                #region servicio tipos servicio
                {4001, "Ya existe un tipo servicio con ese nombre, Por favor escriba otro." },
                {4002, "Ya existe un tipo servicio con ese código de barras, Por favor escriba otro" },
                #endregion
                #region registrar pases
                {4201 , "Empresa invalida para crear pase"} ,
                {4202 , "Sector invalido para crear pase" },
                {4203 , "Tipo de Pase invalido para crear pase"},
                {4204, "Empresa invalida para actualizar pase" },
                {4205, "Sector invalido para actualizar pase" },
                {4206, "Tipo de Pase invalido para actualizar pase" },
                {4207, "Error en el proceso de borrado de pase" },
                {4208, "Error en el proceso de consulta de pase no hay datos de tipoPase pase asociados a la empresa" },
                {4209, "Error La fecha de inicio no puede ser mayor a la fecha fin" },
                #endregion
                #region servicio tipos pases
                {4301,  "Ya existe un tipo pase con ese nombre, Por favor escriba otro." },
                {4302, "Ya existe un tipo servicio con ese código de barras, Por favor escriba otro" },
                {4303,  "La deducción no existe para la empresa." },
                #endregion
                #region genericos zonas,sectores,personal, sectorsectores
                     {3200, "Uno a mas campos estan en null, Por favor verifique." },
                     {3201, "No puede borrar esta lista por que no corresponde a la compania."},
                     {3202, "Hubo un error en el sistema. por favor intente despues."},
                     {3203, "Ya existe un sector con ese nombre. Por favor, escriba otro"},
                #endregion
                #region lugarres
                {4601, "Error." },
                {4602, "Error en procesar informacion KML {0} " },
                {4603, "Error en formatear informacion del KML revisar lugar {0} " },
                {4604, "El sector asociado en el archivo KML no existe o se encuentra inactivo " },
                {4605, "No puede existir dos lugares con el mismo nombre. Por favor revisar los siguientes lugares que estan repetidos: {0} " },
                {4606, "Error en eliminar el lugar " },
                {4607, "No se encontraron lugares según los parámetros enviados " },
                {4608, "La latitud del lugar debe estar entre -90 y 90 " },
                {4609, "La longitud del lugar debe estar entre -180 y 180 " },
                {4610, "Error en crear el lugar " },
                {4611, "Error en actualizar el lugar " },
                {4612, "No existe equivalencia con el código ingresado " },
                {4613, "Error en obtener catálogo  de equivalencias " },
                {4614, "Ya existe un lugar con el nombre {0}" },
                #endregion
                #region Rutas
                {3501,"Error" },
                {3502,"La ruta ya existe" },
                {3503,"Tarifa minima no puede ser 0" },
                {3504,"Tarifa maxima no puede ser 0" },
                {3505,"La tarifa minima no puede ser mayor que la tarifa maxima" },
                {3506,"El porcentaje de marcas a descontar debe estar entre 0 y 100" },
                {3507,"El porcentaje de tarifa maxima a descontar debe estar entre 0 y 100 "},
                {3508,"El porcentaje de tolerancia debe estar entre 0 y 100"},
                {3509,"La escalera superior debe estar entre 0 y 10" },
                {3510,"La escalera inferior debe estar entre 0 y 10" },
                {3511,"El tiempo maximo en ruta debe ser mayor a 60, menor a 12000 en multiplos de 60" },
                {3512,"El tiempo maximo de personas bajando debe ser mayor a 60, menor a 12000 en multiplos de 60" },
                {3513,"El lugar {0} no tiene abreviatura de SIPS configurada" },
                {3514,"Las rutas habilitadas para la boletera deben de tener la misma cantidad de Geocercas con respecto a los lugares " },
                {3515,"No se pudo guardar ruta" },
                {3516,"No se pudo guardar rutaChequeador" },
                {3517,"No se pudo guardar rutaParada" },
                {3518,"No se pudo guardar rutaLugar" },
                {3519,"No se pudo guardar rutaGeocerca" },
                {3520,"No se pudo elimnar registro en rutaChequeador" },
                {3521,"No se pudo elimnar registro en rutaParada" },
                {3522,"No se pudo elimnar registro en rutaLugar" },
                {3523,"No se pudo elimnar registro en rutaGeocerca" },
                {3524,"No se pudo eliminar las tarifas y/o Formulas multitarifarias asociadas a la ruta" },
                {3525,"No se pudo obtener listado de rutas de GFA" },
                {3526,"No existe lugar con la abreviatura de SIPS: {0}" },
                {3527,"No se pudo elimnar registro en ruta" },
                {3528,"No encontraron rutas segun los parametros enviados " },
                {3529,"No encontraron lugares asociados a la ruta " },
                {3530,"No se pudo actualizar la ruta" },
                {3531,"No se encontró mátriz de kilometraje de distancias de la ruta {0}, por favor cree primero la matriz de distancias para crear el tarifario" },
                {3532,"No hay matriz tarifaria para la ruta seleccionada" },
                {3533,"No se pueden calcular las tarifas de manera automática porque faltan las distancias de los siguientes orígenes y destinos: \n {0}" },
                #endregion

               #region Programar parametros
                    {3401, "Uno a mas campos estan nulos o vacios, Por favor verifique." },
                    {3402, "No hay vehiculos para consolidar, Por favor verifique." },
                    {3403, "Hubo un error en el sistema. por favor intente despues." },
                    {3404, "Uno o mas vehiculos no se encuentran. Por favor verifque." },
               #endregion
               
               #region Vehiculos
               {3101, "Ya existe una Lectora, por favor verifique el valor"},
               {3102, "Ya existe un código, por favor verifique el valor" },
               {3103,"Ya existe un contador, por favor verifique el valor" },
               {3104,"No se pudo obtener los autobuses de desde GFA" },
               {3105,"El serial de boletera ya existe para la empresa" },

                #endregion
               #region TipoPasajeros
               {4101, "El nombre ya existe para la empresa"},
               {4102, "El código de barras ya existe para la empresa" },
               {4103,"La deducción no corresponde a la empresa" },
               {4104,"Ya existe una abreviatura de tipo de pasajero para la empresa" },
               {4105,"Error al consultar tipos de pasajeros a GFA" },
               {4106,"Se ha enviado una ruta no válida" },
               {4107,"No hay lugares para la ruta seleccionada" },
                #endregion
                {6201, "Error al consultar los Ids seleccionados" },
                {6001, "Error al generar consulta de reporte" },
                {6301, "Los datos suministrados son invalidos" },
                #region Boletos
                {6501, "Error al conectar con la base de datos de Boletos" },
                {6502, "No fue posible crear o actualizar el boleto" },
                {6503, "No fue posible eliminar el boleto" },
                {6504, "No se permite modificar o eliminar un boleto agregado automáticamente" },
                {6505, "El campo cantidad es obligatorio en un boleto de tipo UNITARIO" },
                {6506, "El campo cantidad y monto por unidad es obligatorio en un boleto de tipo PRODUCTO" },
                {6507, "El campo monto total es obligatorio en un boleto de tipo LIBRE" },
                {6508, "No fue posible obtener boletos de API GFA" },
                {6509, "No se encontro un tipo de boleto valido" },
                {6510, "El numero TCS y Ruta Seleccionada son obligatorios" },
                #endregion
                #region Alertas_Servicios_Ilegales
                {7201, "Error al consultar parámetros necesarios" },
                {7202, "Error al obtener alertas recorridos ilegales" },
                {7203, "Error al consultar datos relacionados a los servicios y rutas seleccionadas de una TCS" },
                {7204, "Error al obtener registros de recorridos para el vehiculo" },
                {7205, "Error al intentar crear una nueva alerta de recorridos ilegales para la TCS" },
                {7206, "Error al intentar eliminar las alertas relacionadas a la TCS" },
                {7207, "El numero de TCS es obligatorio" },
                {7208, "El codigo de vehiculo es obligatorio" },
                {7209, "Error al realizar analisis de recorridos ilegales" },
                #endregion

                {8001, "Error al consultar las TCS existentes" },
                #region personal
                {4300, "No se pudo obtener los conductores desde GFA." },
                #endregion
                {8002, "Error al consultar el detalle de la TCS" },
                {8003, "Error al consultar las contraseñas de la TCS" },
                #region Usuarios
               {3901, "Ya existe un usuario con el código enviado para la empresa, por favor escriba otro"},
               {3902, "Ya existe un usuario con el correo enviado para la empresa, por favor escriba otro" },
               {3903, "El sector o los sectores no existen o no pertenecen a la empresa del usuario" },
                #endregion
                #region Empresas
                {2901, "Ya existe una empresa con ese nombre, Por favor escriba otro." },
                #endregion
                #region Bitacora Sistema
                {4701, "No se encontraron datos con el filtro enviado" },
                #endregion
                #region Ubicacion
                {2902, "Ya existe ese país en la base de datos" },
                {2903, "Ya existe esa ciudad en la base de datos" },
                {2904, "Ya existe ese departamento en la base de datos" },                
                #endregion
          
          
                #region Rutas Seleccionadas
                {5501, "No se permite agregar la ruta porque algunas lineas seleccionadas ya han sido incluidas previamente en otra ruta."},
                {5502, "¡Error campo requerido! Debe ingresar el código del vehículo, campo obligatorio."},
                {5503, "¡Error campo requerido! Debe seleccionar la fecha de inicio, campo obligatorio."},
                {5504, "¡Error campo requerido! Debe seleccionar la fecha final, campo obligatorio."},
                {5505, "¡Error campo requerido! Debe ingresar la placa del vehículo, campo obligatorio."},
                {5506, "¡Error campo requerido! Debe ingresar el Id del Conductor, campo obligatorio."},
                {5507, "¡Error campo requerido! Debe ingresar el Id de la Ruta, campo obligatorio."},
                {5508, "¡Error campo requerido! Debe ingresar el Id del vehículo, campo obligatorio."},
                {5509, "¡Error campo requerido! Debe ingresar la lista de Registros, campo obligatorio."},
                {5510, "¡Error campo requerido! Debe ingresar el Id del conductor actual, campo obligatorio."},
                {5511, "¡Error campo requerido! Debe ingresar el Id del conductor nuevo, campo obligatorio."},
                {5512, "¡Error campo requerido! Debe ingresar la lista de Rutas seleccionadas, campo obligatorio."},

                { 5401,"¡Acceso denegado! Por favor verificar las credenciales de acceso suministrada." },
                { 5402, "¡Error en la conversión numérica! El identificador debe ser un número válido." },
                { 5403, "¡Error campo requerido! Debe seleccionar la fecha de inicio, campo obligatorio." },
                { 5404, "¡Error campo requerido! Debe seleccionar la fecha final, campo obligatorio." },
                { 5405, "¡Campo requerido! Debes seleccionar el IdVehiculo, campo obligatorio." },
                { 5406, "¡Error de conversión de fecha! Formato no válido, la fecha de inicio debe ser una fecha válida." },
                { 5407, "¡Error de conversión de fecha! Formato no válido, la fecha final debe ser una fecha válida." },
                { 5408, "¡Error en la conversión numérica! El IdVehiculo debe ser un número válido." },
                { 5409, "¡Error de campo obligatorio! El idSector es necesario para la ejecución del servicio routeSelectedConsultingRoutes." },
                { 5410, "¡Error en la conversión numérica! El idSector debe ser un número válido." },
                { 5411, "¡Error de campo obligatorio! El idSector es necesario para la ejecución del servicio consultarConductores." },
                { 5412, "¡Error de parámetro de entrada! Lista vacía, se esperan datos en la lista." },
                { 5413, "¡Error en la conversión numérica! El IdRutaSeleccionada  debe ser un número válido." },
                { 5414, "Error in input parameters! the request cannot be empty, a list of Id routes is expected." },
                { 5415, "Error in the request sent, request empty, a list of objects is expected." },
                { 5416, "Could not update the records!" },
                { 5901, "Error. Hay campos faltantes en los parametros de entrada." },
                { 5902, "Error. El formato de la fecha inicial o final es invalido." },
                { 5903, "Error. La fecha de inicio no puede ser mayor o igual a la fecha final." },

                { 5417, "Wrong request! You must select an idTcs required field." },
                { 5418, "Error in numerical conversion! The idTcs must be a valid number." },
                { 6901, "¡Error campo requerido! Debe ingresar el número de la TCS, campo obligatorio." },
                { 6902, "No existe la TCS o el conductor con el codigo enviado." },
                { 6903, "Campos no validos." },
                { 6904, "No se cuenta con los permisos necesarios para ejecutar el servicio Web de GFA" },
                { 6905, "Error en consumo de Web services de GFA" },
                { 7301 , "Error La fecha de inicio no puede ser igual o mayor a la fecha fin" },
                #endregion
                #region Cercanías Lugares
                {5100, "No hay parametros del gps para realizar el cálculo" },
                #endregion

                #region Reporte Liquidacion viajes por servicio
                { 5601,"Error en la ejecución del reporte, por favor verifique los parametros de entrada y la conexion a la BD"},
                #endregion

                #region Zonas
                 {3301, "Ya existe una zona con ese código. Por favor, escriba otro" },
                #endregion
                #region
                 {4501, "El perfil {0} ya existe" },
                #endregion
                #region RevisionTecnica
                {7401, "Error consultando Revision Técnica, por favor revise conexion a BD y que los parametros de entradas no esten nullos"},
                {7402, "Error en el Envio del mensaje Mqtt" },
                {7409, "Error en la Recepción del Mensaje Mqtt"},
                #endregion
                #region tarifarios
                {8100, "Ya existe un tarifario guardado con la misma ruta y fecha de inicio de vigencia, modifique el tarifario ya existente o eliminelo para crear uno nuevo" },
                {8101, "Error en formatear tarifa {0}" },
                {8102, "Error en formatear detalle de tarifa {0}" },
                {8103, "Se tiene que enviar un id de ruta para  poder eliminar el tarifario" },
                {8104, "No se eliminaron registros del tarifario" },
                {8105, "Error en eliminar tarifarios " },
                {8106, "no se encontraron datos de la ruta según el parámetro enviado" },
                {8107, "no hay matriz tarifaria configurada para la ruta {0} " },
                {8108, "La matriz no tiene los requisitos mínimos para ser creada" },
                {8109, "La matriz no tiene los requisitos minimos para poder hacer el calculo por kilometro menor" },
                {8110, "No se logró importar tarifas para la creación de la matriz " },
                {8111, "No existe tipo de pasajero configurado con la abreviatura {0} " },
                {8112, "No se puede pasar listado de tarifas a un arreglo bidimensional" },
                #endregion


            };
        }

        public Dictionary<int, object> diccionarioEN()
        {
            return new Dictionary<int, object>()
            {
                {1000, "Error in DataBase"},
                {2001, "A deduction already exists with that name, Please enter another." },
                {2002, "The register already exist in the table" },
                #region login
                {2601, "Invalid login. Please check credentials"},
                #endregion
               #region genericos zonas,sectores,personal, sectorsectores
                {3200, "There are one or more fields in 0 or null, please verify." },
                {3201, "You can't delete this list because not corresponding to the company." },
                {3202, "There was an data exception. Please try again later."},
                {3203, "A sector with that name already exists. please write another"},
                #endregion
                #region servicio tipos servicio
                {4001, "A type service already exist whit that name, Please enter another." },
                {4002, "There is already a serious type with that barcode, Please enter another" },
                #endregion
                 #region registrar pases
                {4201 , "Invalid company for create pass" },
                {4202 , "Invalid sector for create pass" },
                {4203 , "Invalid type of pass for create pass" },
                {4204, "Invalid company for update pass"},
                {4205, "Invalid sector for update pass"},
                {4206, "Invalid type of pass for update pass"},
                {4207, "Error in procces of delete pass"},
                {4208, "Error in procces of consult pass, no associated TypePass data to the company"},
                {4209, "Error The start date cannot be greater than the end date"},

                #endregion
                #region servicio tipos pases
                {4301,  "A type service already exist whit that name, Please enter another." },
                {4302, "There is already a serious type with that barcode, Please enter anothe" },
                {4303, "The deduction does not exist for the company" },
                #endregion
                #region Vehiculos
                {3101, "There is already a Reader, please check the value"},
                {3102, "A code already exists, please check the value" },
                {3103,"A counter already exists, please check the value" },
                {3104, "Could not get buses from from GFA"},
                #endregion
                #region Programar parametros
                {3401, "There are one or more fields in 0 or null, please verify." },
                {3402, "There isn't vehicles for consolidate. Please verify." },
                {3403, "There was an data exception. Please try again later." },
                {3404, "One or more vehicles can't be found. Please verify." },
                #endregion
                #region TipoPasajeros
                {4101,"The name already exists for the company"},
                {4102,"The barcode already exists for the company" },
                {4103,"The deduction does not correspond to the company" },
                {4104,"You already have a passenger type abbreviation for the company" },
                {4105,"Error when consulting passenger types to GFA" },
                {4106,"Invalid route has been sent" },
                {4107,"There are no places for the selected route" },
                #endregion
                #region lugares
                {4601, "Error." },
                {4602, "Error in processing KML information {0} " },
                {4603, "Error in formatting KML information, check place {0} " },
                {4604, "The associated sector in the KML file does not exist or is inactive " },
                {4605, "There cannot be two places with the same name. Please check the following places that are repeated: {0} " },
                {4606, "Failed to delete the place " },
                {4607, "No places were found according to the parameters sent " },
                {4608, "The latitude of the place must be between -90 and 90 " },
                {4609, "Place longitude must be between -180 and 180 " },
                {4610, "Failed to create the place " },
                {4611, "Failed to update the place " },
                {4612, "There is no equivalence with the entered code " },
                {4613, "Error in obtaining equivalence catalog " },
                {4614, "There is already a place with the name {0}" },
                #endregion
                #region Rutas
                {3501,"Error" },
                {3502,"Error The route already exists" },
                {3503,"Minimum rate cannot be 0" },
                {3504,"Maximum rate cannot be 0" },
                {3505,"The minimum rate cannot be higher than the maximum rate" },
                {3506,"The percentage of brands to be discounted must be between 0 and 100" },
                {3507,"The maximum rate percentage to be discounted must be between 0 and 100"},
                {3508,"The tolerance percentage must be between 0 and 100"},
                {3509,"The top ladder must be between 0 and 10" },
                {3510,"The bottom ladder must be between 0 and 10" },
                {3511,"The maximum time en route must be greater than 60, less than 12000 in multiples of 60" },
                {3512,"The maximum time of people getting off must be greater than 60, less than 12000 in multiples of 6" },
                {3513,"Location {0} does not have SIPS abbreviation configured" },
                {3514,"The routes enabled for the ticket office must have the same number of Geofences with respect to the places " },
                {3515,"Could not save Route" },
                {3516,"Could not save Route checker" },
                {3517,"Could not save Route strop" },
                {3518,"Could not save Route place" },
                {3519,"Could not save ruta Geocerca" },
                {3520,"Could not delete record in Route checker" },
                {3521,"Could not delete record in Route strop" },
                {3522,"Could not delete record in Route place" },
                {3523,"Could not delete record in Route geocerca" },
                {3524,"It was not possible to eliminate the rates and / or multi-tariff forms associated with the route" },
                {3525,"Could not get list of routes from GFA" },
                {3526,"There is no place with the abbreviation of SIPS: {0}" },
                {3527,"Could not delete record in Route" },
                {3528,"They did not find routes according to the parameters sent " },
                {3529,"They do not find places associated with the route " },
                {3530,"Failed to update route" },
                {3531,"No distance mileage matrix for route {0} was found, please create the distance matrix first to create the fare" },
                {3532,"There is no fare matrix for the selected route" },
                {3533,"Unable to calculate fares automatically because distances are missing for the following origins and destinations: \n {0}"},
                #endregion
                #region Reporte Analisis Servicios
                { 6101,"Error en la ejecución del reporte, por favor verifique los parametros de entrada y la conexion a la BD"},
                #endregion

                #region personal
                {4300, "Could not get drivers from GFA." },
                #endregion
                {6201, "Error querying selected Ids" },
                {6001, "Error generating query report" },
                {6301, "The data provided is invalid" },
                #region Boletos
                {6501, "Error connecting to Tickets database" },
                {6502, "Unable to create or update ticket" },
                {6503, "Unable to remove ticket" },
                {6504, "Does not allow you to modify or delete an automatically added ticket" },
                {6505, "The quantity field is mandatory on a UNITARIO type ticket" },
                {6506, "The quantity and amount per unit field is mandatory on a PRODUCTO type ticket" },
                {6507, "The total amount field is mandatory on a LIBRE type ticket" },
                {6508, "Unable to get API GFA tickets" },
                {6509, "No valid ticket type found" },
                {6510, "The TCS number and Selected Route are mandatory" },
                #endregion
                #region Alertas_Servicios_Ilegales
                {7201, "Failed to query required parameters" },
                {7202, "Failed to get illegal tour alerts" },
                {7203, "Failed to query data related to selected services and routes of a TCS" },
                {7204, "Error obtaining route records for the vehicle" },
                {7205, "Error trying to create a new illegal tour alert for TCS" },
                {7206, "Failed to delete TCS-related alerts" },
                {7207, "The TCS number is mandatory" },
                {7208, "The vehicle code is mandatory" },
                {7209, "Error when performing analysis of illegal routes" },
                #endregion
                {8001, "Error querying existing TCS" },
                {8002, "Error querying the TCS detail" },
                {8003, "Error querying the TCS passwords" },
                #region Usuarios
               {3901, "There is already a user with the code sent to the company, please write another"},
               {3902, "There is already a user with the email sent to the company, please write another" },
               {3903, "The sector or sectors do not exist or do not belong to the user's company" },
                #endregion

                 #region Empresas
                {2901, "There is already a company with that name, please write another one." },
                #endregion

                #region Rutas Seleccionadas
                {5501, "It is not allowed to add the route because some selected lines have already been previously included in another route."},
                {5502, "Error required field! You must enter the vehicle code, required field."},
                {5503, "Error required field! You must select the start date, required field."},
                {5504, "Error required field! You must select the end date, required field."},
                {5505, "Error required field! You must enter the vehicle registration number, required field."},
                {5506, "Error required field! You must enter the driver id, required field."},
                {5507, "Error required field! You must enter the path id, required field."},
                {5508, "Error required field! You must enter the vehicle id, required field."},
                {5509, "Error required field! You must enter the registers list, required field."},
                {5510, "Error required field! You must enter the current driver Id, required field."},
                {5511, "Error required field! You must enter the new driver Id, required field."},
                {5512, "Error required field! You must enter the list of selected routes, required field."},
                { 5401, "Access denied! Please verify the access credentials provided." },
                { 5402, "Error in numerical conversion! Identificador must be a valid number." },
                { 5403, "Error required field! You must select the start date, required field." },
                { 5404, "Error required field! You must select the end date, required field." },
                { 5405, "Required field! You must select the IdVehiculo, required field."},
                { 5406, "Date conversion error! Invalid format, the start date must be a valid date."},
                { 5407, "Date conversion error! Invalid format, the end date must be a valid date."},
                { 5408, "Error in numerical conversion! The IdVehiculo must be a valid number."},
                { 5409, "Mandatory field error! The idSector is required for the execution of the service routesSelectedConsultingRoutes."},
                { 5410, "Error in numerical conversion! The idSector must be a valid number."},
                { 5411, "Mandatory field error! The idSector is required for the execution of the service consultarConductores."},
                { 5412, "Input parameter error! Empty list, data is expected in the list."},
                { 5413, "Number conversion error! The IdRutaSelected must be a valid number."},
                { 5414, "¡Error en los parámetros de entrada! la solicitud no puede estar vacía, se espera una lista Id rutas" },
                { 5415, "Error en la petición enviada, petición vacía , se espera una lista de objetos" },
                { 5416, "¡No se pudieron actualizar los registros!" },
                { 5901, "Error. There are missing fields in the input parameters." },
                { 5902, "Error. The start date cannot be greater than or equal to the end date." },
                { 5903, "Error. The date format is invalid." },
                { 5417, "¡Solicitud incorrecta! Debe seleccionar el campo obligatorio id Tcs." },
                { 5418, "¡Error en la conversión numérica! El idTcs debe ser un número válido." },
                { 6901, "Error required field! You must enter the TCS number, required field." },
                { 6902, "The TCS or the driver with the code sent does not exist." },
                { 6903, "Invalid fields." },
                { 6904, "You do not have the necessary permissions to run the Web services GFA" },
                { 6905, "GFA Web services consumption error" },
                { 7301 , "Error The start date cannot be equal to or greater than the end date" },
	            #endregion
          
                #region Bitacora Sistema
                {4701, "No data was found with the submitted filter" },                
                #endregion
                 #region Ubicacion
                {2902, "That country already exists in the database" },
                {2903, "That city already exists in the database" },
                {2904, "That department already exists in the database" },
                #endregion
                #region Cercanías Lugares
                {5100, "There are no gps parameters to perform the calculation" },
                #endregion
                #region Reporte Liquidacion viajes por servicio
                { 5601,"Error in the execution of the report, please check the input parameters and the connection to the DB"},
                #endregion
                #region Zonas
                 {3301, "There is already an area with that code. please write another" },
                #endregion
                 #region RevisionTecnica
                { 7401, "Error consulting Technical Review, please check connection to DB and that the input parameters are not null"},
                {7402, "Error sending Mqtt message" },
                {7403, "Failed to Receive Mqtt Message"},
                #endregion
                #region                 
                 {4501, "Profile {0} already exists" },
                #endregion
                #region tarifarios
                {8100, "There is already a saved tariff with the same route and effective date, modify the existing tariff or delete it to create a new one" },
                {8101, "Fare formatting error {0}"},
                {8102, "Fare detail formatting error {0}"},
                {8103, "You have to send a route id to be able to eliminate the tariff"},
                {8104, "Tariff records were not deleted"},
                {8105, "Error in deleting tariffs"},
                {8106, "no route data found based on sent parameter"},
                {8107, "there is no fare matrix configured for route {0}"},
                {8108, "The matrix does not have the minimum requirements to be created"},
                {8109, "The matrix does not have the minimum requirements to be able to make the calculation per minor kilometer"},
                {8110, "It was not possible to import rates for the creation of the matrix"},
                {8111, "There is no passenger type configured with the abbreviation {0} "},
                {8112, "It is not possible to pass list of rates to a two-dimensional array"},
                #endregion
            };
        }
    }
}
