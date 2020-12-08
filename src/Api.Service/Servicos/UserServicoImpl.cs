using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.User;
using Api.Domain.Models.UserModels;
using AutoMapper;

namespace Api.Service.Servicos
{
    public class UserServicoImpl : IUserService
    {
        private IRepositorio<UserEntity> _repositorioUser;
        private readonly IMapper _mapper;

        public UserServicoImpl(IRepositorio<UserEntity> repositorioUser, IMapper mapper)
        {
            _repositorioUser = repositorioUser;
            _mapper = mapper;
        }
        
        public async Task<bool> Delete(Guid id)
        {
            return await _repositorioUser.DeleteAsync(id);
        }

        public async Task<UserDtoCreate> Get(Guid id)
        {
            var usuarioEntity = await _repositorioUser.SelecioneAsync(id);
            return _mapper.Map<UserDtoCreate>(usuarioEntity);
        }

        public async Task<IEnumerable<UserDtoCreate>> GetAll()
        {
            var listaDeUsuarioEntity =  await _repositorioUser.SelecioneListaAsync();
            return _mapper.Map<IEnumerable<UserDtoCreate>>(listaDeUsuarioEntity);
        }

        public async Task<UserDtoCreateResult> Post(UserDtoCreate userDto)
        {
            var usuarioModel = _mapper.Map<UserModel>(userDto);
            var usuarioEntity = _mapper.Map<UserEntity>(usuarioModel);
            var resultado = await _repositorioUser.SalvarAsync(usuarioEntity);            

            return _mapper.Map<UserDtoCreateResult>(resultado);
        }

        public async Task<UserDtoUpdateResult> Put(UserDtoUpdate userDto)
        {
            var usuarioModel = _mapper.Map<UserModel>(userDto);
            var usuarioEntity = _mapper.Map<UserEntity>(usuarioModel);
            var resultado = await _repositorioUser.AtualizeAsync(usuarioEntity);            

            return _mapper.Map<UserDtoUpdateResult>(resultado);
        }
    }
}
