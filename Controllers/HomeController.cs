using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GlobalTec.Models; //pegar as models presentes no projeto
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using GlobalTec.Services; // pegar service de Autenticação
using GlobalTec.Repositories; // pegar repositorio de usuario 

namespace GlobalTec.Controllers
{
  [Route("v1/account")]
  public class HomeController : Controller
  {


    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
    {
      // Recupera o usuário
      var user = UserRepository.Get(model.UserName, model.Password);

      // Verifica se o usuário existe
      if (user == null)
        return NotFound(new { message = "Usuário ou senha inválidos" });

      // Gera o Token
      var token = TokenService.GenerateToken(user);

      // Oculta a senha
      user.Password = "";

      // Retorna os dados
      return new
      {
        user = user,
        token = token
      };
    }


    [HttpGet]
    [Route("anonymous")]
    [AllowAnonymous]
    public string Anonymous() => "Anônimo";

    //--------------------------------------------------------------------------
    //precisa passar no header 
    //key: Authorization
    //value: Bearer TOKEN
    //--------------------------------------------------------------------------

    [HttpGet]
    [Route("authenticated")]
    [Authorize]
    public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

    [HttpGet]
    [Route("employee")]
    [Authorize(Roles = "employee,manager")]
    public string Employee() => "Funcionário";

    [HttpGet]
    [Route("manager")]
    [Authorize(Roles = "manager")]
    public string Manager() => "Gerente";

  }
}