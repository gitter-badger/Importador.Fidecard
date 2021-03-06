using System;
using Castle.ActiveRecord;
using FileProccesor.Dtos.ValueTypes;
using FileProccesor.Keys;

namespace FileProccesor.Dtos
{
    [ActiveRecord("Personas")]
    public class PersonaDto : ActiveRecordValidationBase<PersonaDto>
    {
        [CompositeKey]
        public KeyPersona Key { get; set; }

        [Property("Nombre")]
        public string Nombre { get; set; }

        [Property("Apellido")]
        public string Apellido { get; set; }

        [Property("Domicilio", Length = 50)]
        public string Domicilio { get; set; }

        [BelongsTo("CodPostal")]
        public CodigoPostalDto CodPostal { get; set; }

        [Property("Telefono", Length = 14)]
        public string Telefono { get; set; }

        [Property("celular", Length = 16)]
        public string Celular { get; set; }

        [Property("fax", Length = 14)]
        public string Fax { get; set; }

        [Property("email", Length = 50)]
        public string Email { get; set; }

        [Property("sexo", Length = 9)]
        public string Sexo { get; set; }

        [Nested]
        public FechaNacimiento FechaNacimientoPersona { get; set; }

        [Property("actividad")]
        public string ActividadPersona { get; set; }

        [Property("FechaBaja")]
        public DateTime? FechaBaja { get; set; }

        [Property("CodMotivoBaja")]
        public int MotivoBaja { get; set; }

        [Property("Cuenta",Length = 10)]
        public string Cuenta { get; set; }
    }
}