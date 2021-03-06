using System;
using System.Reflection;
using Castle.ActiveRecord;
using FileProccesor.Dtos;
using FileProccesor.Keys;
using FileProccesor.Services.Helpers;
using log4net;

namespace FileProccesor.Services
{
    public static class WorkflowFidecard
    {
        private static readonly ILog Log
           = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static void Registro()
        {
            foreach (var archivo in ActiveRecordBase<ConsumoDto>.FindAllByProperty("Procesado", false))
            {
                var documento = HelperPersona.GetPersona(
                    archivo.Cuit, archivo.TipoCliente,
                    archivo.RazonSocial, archivo.NombrePersona,
                    archivo.NroDocumento, archivo.Empresa);

                    var cliente = HelperCuenta.GetCuenta(
                        archivo.Cuit, archivo.NroDocumento, archivo.Empresa);

                    using (var transac = new TransactionScope())
                        try
                        {
                            var puntos = HelperPuntos.GetPuntos(archivo.Empresa, archivo.FechaHoraComprobante,
                                                            archivo.ImportePesosNetoImpuestos);

                            double acelerador = Double.Parse(archivo.Coeficiente) / 100;
                            puntos = acelerador > 0 ? acelerador * puntos : puntos;

                            var cuenta = new CuentaCorrienteDto
                            {
                                FechaCompra = archivo.FechaHoraComprobante.Date,
                                HoraCompra = DateTime.Now,
                                Key = new KeyCuenta
                                {
                                    CodEmpresa = archivo.Empresa,
                                    NumeroComprobante = archivo.NroComprobante
                                },
                                MontoCompra = archivo.ImportePesosNetoImpuestos,
                                Movimiento = puntos >= 0 ? HelperMovimiento.FindMovimiento("Suma De Puntos") : HelperMovimiento.FindMovimiento("Anulación Carga"),
                                NumeroDocumento = documento,
                                NumeroCuenta = cliente,
                                Puntos = puntos,
                                Sucursal = HelperSucursal.GetSucursal(),
                                Usuario = "web",
                                Programa = archivo.Programa,
                                Secretaria = archivo.Secretaria,
                                Coeficiente = archivo.Coeficiente
                            };
                            cuenta.Save();
                            transac.VoteCommit();
                        }
                        catch (Exception ex)
                        {
                            archivo.Error = ex.Message;
                            Log.Fatal(ex);
                            transac.VoteRollBack();
                        }
                    archivo.Procesado = true;
                    archivo.Save();
                } 
            }
        }
    }
