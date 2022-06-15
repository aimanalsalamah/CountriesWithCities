using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedsController : ControllerBase
    {
        private readonly API.Data.ApiContext _context;
        public SeedsController(API.Data.ApiContext context)
        {
            this._context = context;
        }
        [HttpGet("Fill")]
        public async Task<IActionResult> GetSeeds()
        {
            try
            {
                _context.Countries.AddRange(new BLL.Seeds.Countries().Get());
                var result = await _context.SaveChangesAsync();
                return Ok($"{result} rows have been inserted.");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message + ex.ToString());
            }
        }

        [HttpGet("Delete")]
        public ActionResult GetSeedsDelete()
        {
            try
            {
                var DeleteResult = _context.Database.ExecuteSqlRaw("DECLARE @sqlDROPCONSTRAINT NVARCHAR(MAX); SET @sqlDROPCONSTRAINT = N''; SELECT @sqlDROPCONSTRAINT = @sqlDROPCONSTRAINT + N' ALTER TABLE ' + QUOTENAME(s.name) + N'.' + QUOTENAME(t.name) + N' DROP CONSTRAINT ' + QUOTENAME(c.name) + ';' FROM sys.objects AS c INNER JOIN sys.tables AS t ON c.parent_object_id = t.[object_id] INNER JOIN sys.schemas AS s ON t.[schema_id] = s.[schema_id] WHERE c.[type] IN ('D','C','F','PK','UQ') ORDER BY c.[type]; EXEC sys.sp_executesql @sqlDROPCONSTRAINT; DECLARE @sqlDroptable NVARCHAR(max)='' SELECT @sqlDroptable += ' Drop table ' + QUOTENAME(s.NAME) + '.' + QUOTENAME(t.NAME) + '; ' FROM   sys.tables t JOIN sys.schemas s ON t.[schema_id] = s.[schema_id] WHERE  t.type = 'U' Exec sp_executesql @sqlDroptable");
            }
            catch (Exception ex) { return Ok(ex.ToString()); }
            if ((_context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
            {
                _context.Database.EnsureCreated();
            }
            return Ok("Success delete.");
        }

        [HttpGet("Create")]
        public ActionResult Create()
        {
            try
            {
                _context.Database.EnsureCreated();
            }
            catch (Exception ex) { return Ok(ex.ToString()); }
            return Ok("Success Create.");
        }
    }
}