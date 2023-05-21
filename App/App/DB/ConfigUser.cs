using App.Response;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TableAttribute = SQLite.TableAttribute;

namespace App.DB
{
    [Table("configuser.db3")]
    public class ConfigUser : Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int idUserDB { get; set; }
    }
}
