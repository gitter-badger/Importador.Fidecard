using System;
using Castle.ActiveRecord;
using FileProccesor.Schemes;

namespace FileProccesor.Dtos
{
    [ActiveRecord]
    public class ConsumoDto : ActiveRecordBase<ConsumoDto>
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [Property]
        public string Cuit { get; set; }
       
        [Property]
        public string RazonSocial { get; set; }
       
        [Property]
        public string NroDocumento { get; set; }
        
        [Property]
        public string NombrePersona { get; set; }
        
        [Property]
        public string TipoCliente { get; set; }
        
        [Property(Unique = true)]
        public string NroComprobante { get; set; }
        
        [Property]
        public DateTime FechaHoraComprobante { get; set; }
        
        [Property]
        public double ImportePesosNetoImpuestos { get; set; }

        public static implicit operator ConsumoDto(TransatlanticaFile file)
        {

            return new ConsumoDto
                       {
                           Cuit = file.Cuit,
                           FechaHoraComprobante = file.FechaHoraComprobante,
                           ImportePesosNetoImpuestos = file.ImportePesosNetoImpuestos,
                           NombrePersona = file.NombrePersona,
                           NroComprobante = file.NroComprobante,
                           NroDocumento = file.NroDocumento,
                           RazonSocial = file.RazonSocial,
                           TipoCliente = file.TipoCliente,
                           };
        }
    }
}

    