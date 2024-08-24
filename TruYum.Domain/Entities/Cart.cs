using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruYum.Domain.Entities
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int UserId {  get; set; }
        public int MenuItemId {  get; set; }
        public string MenuItemName { get; set; }
    }
}
