using FluentValidation;
using Repository.Data;
using System.Threading.Tasks;

namespace api.clientes.Validators
{
    public class ClienteValidator : AbstractValidator<ClienteModel>
    {
        private readonly ICliente _clienteRepository;

        public ClienteValidator(ICliente clienteRepository)
        {
            _clienteRepository = clienteRepository;

            RuleFor(cliente => cliente.nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.");

            RuleFor(cliente => cliente.apellido)
                .NotEmpty().WithMessage("El apellido es obligatorio.")
                .MinimumLength(3).WithMessage("El apellido debe tener al menos 3 caracteres.");

            RuleFor(cliente => cliente.celular)
                .NotEmpty().WithMessage("El celular es obligatorio.")
                .Matches(@"^\d{10}$").WithMessage("El celular debe tener 10 dígitos.");

            RuleFor(cliente => cliente.documento)
                .NotEmpty().WithMessage("El documento es obligatorio.")
                .MinimumLength(7).WithMessage("El documento debe tener al menos 7 caracteres.")
                .MustAsync(async (documento, cancellation) => !(await DocumentoExiste(documento)))
                .WithMessage("El documento ya está registrado.");

            RuleFor(cliente => cliente.mail)
                .NotEmpty().WithMessage("El mail es obligatorio.")
                .EmailAddress().WithMessage("El mail debe tener un formato válido.");

            RuleFor(cliente => cliente.estado)
                .Equal("activo").WithMessage("El cliente debe estar activo para obtener los datos.");
        }

        private async Task<bool> DocumentoExiste(string documento)
        {
            return await _clienteRepository.DocumentoExiste(documento);
        }
    }
}