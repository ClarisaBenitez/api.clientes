using FluentValidation;
using Repository.Data;

namespace api.clientes.Validators
{
    public class FacturaValidator : AbstractValidator<FacturaModel>
    {
        public FacturaValidator()
        {
            RuleFor(factura => factura.nro_factura)
                .Matches(@"^\d{3}-\d{3}-\d{6}$")
                .WithMessage("El número de factura debe tener el formato 'XXX-XXX-XXXXXX', donde 'X' es un dígito numérico.");

            RuleFor(factura => factura.total)
                .NotEmpty().WithMessage("El total es obligatorio.")
                .GreaterThan(0).WithMessage("El total debe ser un número positivo.");

            RuleFor(factura => factura.total_iva5)
                .NotEmpty().WithMessage("El total del IVA 5% es obligatorio.")
                .GreaterThanOrEqualTo(0).WithMessage("El total del IVA 5% debe ser un número no negativo.");

            RuleFor(factura => factura.total_iva10)
                .NotEmpty().WithMessage("El total del IVA 10% es obligatorio.")
                .GreaterThanOrEqualTo(0).WithMessage("El total del IVA 10% debe ser un número no negativo.");

            RuleFor(factura => factura.total_iva)
                .NotEmpty().WithMessage("El total del IVA es obligatorio.")
                .GreaterThanOrEqualTo(0).WithMessage("El total del IVA debe ser un número no negativo.");

            RuleFor(factura => factura.total_letras)
                .NotEmpty().WithMessage("El total en letras es obligatorio.")
                .MinimumLength(6).WithMessage("El total en letras debe tener al menos 6 caracteres.");
        }
    }
}