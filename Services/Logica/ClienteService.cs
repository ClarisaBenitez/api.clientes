using FluentValidation;
using FluentValidation.Results;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Logica
{
    public class ClienteService
    {
        private readonly ICliente _clienteRepository;
        private readonly IValidator<ClienteModel> _clienteValidator;

        public ClienteService(ICliente clienteRepository, IValidator<ClienteModel> clienteValidator)
        {
            _clienteRepository = clienteRepository;
            _clienteValidator = clienteValidator;
        }

        public async Task<bool> Add(ClienteModel cliente)
        {
            ValidationResult validationResult = await _clienteValidator.ValidateAsync(cliente);
            if (!validationResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }

            try
            {
                return await _clienteRepository.add(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar cliente", ex);
            }
        }

        public async Task<bool> Update(ClienteModel cliente)
        {
            ValidationResult validationResult = await _clienteValidator.ValidateAsync(cliente);
            if (!validationResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }

            try
            {
                return await _clienteRepository.update(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar cliente", ex);
            }
        }

        public async Task<ClienteModel> Get(int id)
        {
            try
            {
                return await _clienteRepository.get(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener cliente", ex);
            }
        }

        public async Task<IEnumerable<ClienteModel>> List()
        {
            try
            {
                return await _clienteRepository.list();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar clientes", ex);
            }
        }

        public async Task<bool> Remove(int id)
        {
            try
            {
                return await _clienteRepository.remove(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar cliente", ex);
            }
        }
    }
}