using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GlobalTec.Data;
using GlobalTec.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace GlobalTec.Controllers
{
  [Route("v1/users")]
  public class UserController : Controller
  {
    [HttpGet]
    [Route("list")]
    [Authorize]
    public async Task<ActionResult<List<User>>> Get([FromServices] DataContext context)
    {
      var user = await context.User.ToListAsync();
      return user;
    }

    //create method ------------------------------------------------------------
    [HttpPost]
    [Route("create")]
    [Authorize]
    public async Task<ActionResult<User>> Post(
      [FromServices] DataContext context,
      [FromBody] User model)
    {
      try
      {
        if (ModelState.IsValid)
        {
          context.User.Add(model);
          await context.SaveChangesAsync();
          return model;
        }
        else
        {
          return BadRequest(ModelState);
        }

      }
      catch (System.Exception)
      {
        throw;
      }

    }
    //--------------------------------------------------------------------------

    //list by userCode ---------------------------------------------------------
    [HttpGet]
    [Route("list/userCode")]
    [Authorize]
    public async Task<ActionResult<List<User>>> GetByCode(
      [FromServices] DataContext context,
      string code
      )
    {
      var user = await context.User.AsNoTracking().Where(x => x.UserCode == code).ToListAsync();

      return user;
    }

    //--------------------------------------------------------------------------
    //list by UF ---------------------------------------------------------------
    [HttpGet]
    [Route("list/userUf")]
    [Authorize]
    public async Task<ActionResult<List<User>>> GetByUf(
      [FromServices] DataContext context,
      string Uf
      )
    {
      var user = await context.User.AsNoTracking()
      .Where(x => x.UF == Uf)
      .ToListAsync();

      return user;
    }
    //--------------------------------------------------------------------------

    //update user ---------------------------------------------------------------
    [HttpPut]
    [Route("update/{id:int}")]
    [Authorize]
    public async Task<ActionResult<User>> Put(
      [FromServices] DataContext context,
      int id,
      [FromBody] User model
      )
    {
      // Verifica se os dados são válidos
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      // Verifica se o ID informado é o mesmo do modelo
      if (id != model.Id)
        return NotFound(new { message = "Usuário não encontrada" });

      try
      {
        context.Entry(model).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return model;
      }
      
      catch (Exception)
      {
        return BadRequest(new { message = "Não foi possível atualizar o usuário" });

      }
    }
    //--------------------------------------------------------------------------

    // delete user -------------------------------------------------------------
    [HttpDelete]
    [Route("delete/{id:int}")]
    [Authorize]
    public async Task<ActionResult<User>> Delete(
      [FromServices] DataContext context,
      int id)
    {
      var user = await context.User.FirstOrDefaultAsync(x => x.Id == id);
      if (user == null)
        return NotFound(new { message = "Usuário não encontrada" });

      try
      {
        context.User.Remove(user);
        await context.SaveChangesAsync();
        return StatusCode(200, Json("User Deleted"));
      }
      catch (Exception)
      {
        return BadRequest(new { message = "Não foi possível remover o usuário." });

      }
    }

  }
}