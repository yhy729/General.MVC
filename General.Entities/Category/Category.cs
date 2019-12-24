using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace General.Entities
{
    [Table("Category")]
    public class Category
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="")]
        public string Name { get; set; }

        public bool IsMenu { get; set; }

        public string SysResource { get; set; }

        public string ResourceId { set; get; }

        public string FatherResource { set; get; }

        public string FatherId { set; get; }

        public string Controller { set; get; }

        public string Action { set; get; }

        public string RouteName { get; set; }

        public string CssClass { get; set; }

        public int Sort { set; get; }

        public bool IsDisabled { set; get; }
    }
}
