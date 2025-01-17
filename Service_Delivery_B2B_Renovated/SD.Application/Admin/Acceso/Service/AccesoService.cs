using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using SD.Application.Admin.Acceso.DTOs;
using SD.Application.Admin.User.DTOs;
using SD.Application.Admin.User.Exceptions;
using SD.Application.Common.Exceptions;
using SD.Application.Helpers.Security;
using SD.Application.Interfaces.Helpers.Security;
using SD.Application.Interfaces.Services.Admin.Acceso;
using SD.Domain.Entities.Administracion.Accesos.Roles;
using SD.Domain.Entities.Administracion.User;
using SD.Domain.Interfaces.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static SD.Application.Admin.User.DTOs.GetUsuario;
using GetUsuarioInfoDTO = SD.Application.Admin.Acceso.DTOs.GetUsuarioInfoDTO;

namespace SD.Application.Admin.Acceso.Service
{
    public class AccesoService : IAccesoService
    {
        private IGenericRepository<TbUsuario> _tbUsuarioRepository;
        private IGenericRepository<TbRolPermiso> _tbRolesRepository;
        private readonly IMapper _mapper;
        private ISecretHasher _secretHasher;

        public AccesoService(IGenericRepository<TbUsuario> tbUsuarioRepository, IGenericRepository<TbRolPermiso> tbRolesRepository, ISecretHasher secretHasher, IMapper mapper)
        {
            _tbUsuarioRepository = tbUsuarioRepository;
            _tbRolesRepository = tbRolesRepository;
            _secretHasher = secretHasher;
            _mapper = mapper;
        }

        public async Task<GetUsuarioInfoDTO> Login(CreateLoginDTO rc)
        {
            try
            {
                List<Claim> claims = new List<Claim>();
                int counter = 0;

                var userValidated = ValidateUserLogin(rc) ?? throw new UserNotValidException();

                var rolesList = _tbRolesRepository.VerifyDataExistenceAsync(u => u.NombreUsu == userValidated.NombreUsu).Result.ToList();

                claims.Add(new Claim(ClaimTypes.Name, userValidated.NombreUsu));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userValidated.CodigoUsu));

                if (userValidated.CodigoDep == "Administrador") { claims.Add(new Claim(ClaimTypes.Role, "ADM")); };

                foreach (var rp in rolesList)
                {
                    if (claims.Count != 0)
                    {
                        if (!claims[counter].Equals(new Claim(ClaimTypes.Role, rp.Rol)) && !claims[counter].Equals(new Claim("Access", rp.Permiso)))
                        {
                            string permission = rp.Rol + "-" + rp.Permiso;
                            claims.Add(new Claim(ClaimTypes.Role, rp.Rol));
                            claims.Add(new Claim("Access", permission));
                        };
                    }
                    else
                    {
                        string permission = rp.Rol + "-" + rp.Permiso;
                        claims.Add(new Claim(ClaimTypes.Role, rp.Rol));
                        claims.Add(new Claim("Access", permission));
                    }
                    counter++;
                }
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var userInfo = _mapper.Map<GetUsuarioInfoDTO>(userValidated);
                userInfo.claims = claimsIdentity;

                return userInfo;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new LoginFailedException();
            }
        }

        public Task LogOut()
        {
            throw new NotImplementedException();
        }

        private TbUsuario ValidateUserLogin(CreateLoginDTO rc)
        {
            try
            {
                TbUsuario user = _tbUsuarioRepository.VerifyDataExistenceAsync(item => item.LoginUsu == rc.LoginUsu && item.Estado == "ACTIVO").Result.FirstOrDefault();

                if(_secretHasher.Verify(rc.ClaveUsu, user.ClaveUsu))
                {
                    return user;
                }
                else
                {
                    throw new WrongPasswordException();
                }

                //return _secretHasher.Verify(rc.ClaveUsu, user.ClaveUsu) ? _mapper.Map<GetUsuarioDTO>(user) : throw new WrongPasswordException();
            }
            catch
            {
                throw;
            }
        }

        public GetUsuarioSSHDTO ValidateUserSSH(string userName)
        {

            try
            {
                // Busca al usuario en la base de datos
                var usuario = _tbUsuarioRepository.VerifyDataExistenceAsync(item => item.NombreUsu == userName && item.Estado == "ACTIVO").Result.SingleOrDefault();

                //string clave = usuario.ClaveTacas;

                return usuario == null ? throw new ValidateUserFailedException() : _mapper.Map<GetUsuarioSSHDTO>(usuario);

                //usuario.ClaveTacas = encrypter.Decrypt(usuario.ClaveTacas);
            }
            catch
            {
                throw;
            }
        }
    }
}
