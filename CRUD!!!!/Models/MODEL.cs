using CRUD____.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD____.Models
{
    public class MODEL
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="*")]
       // [Display(Name ="USER NAME")]
      //  [StringLength(20,ErrorMessage ="Name cant not be Exceed")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Father's NAME")]
        [StringLength(25, ErrorMessage = "Name cant not be Exceed")]
        public string Fname { get; set; }
        [Required]
        [Display(Name = "Mother's NAME")]
        [StringLength(25, ErrorMessage = "Name cant not be Exceed")]
        public string Mname { get; set; }

        public string MOB { get; set; }
        public string Qf { get; set; }
        public string msg { get; set; }


        public int StateId { get; set; }
        public int CityId { get; set; }


        public string gender { get; set; }
        public string ImagePath { get; set; }

        public int StateId1 { get; set; }
        public int CityId1 { get; set; }


        public string City { get; set; }
        public string State { get; set; }

   }
    public class MODEL1
    {
        public int MasterId { get; set; }
        public string MasterName { get; set; }


    }
    public class MODEL2
    {
        DataAccess Db = new DataAccess();
        public SelectList LC(int procid)
        {
            return new SelectList(Db.Reach1(procid).ToList(), "MasterId", "MasterName");
        }
        public SelectList LS(int id,int procid)
        {
            return new SelectList(Db.Reach2(id,procid).ToList(), "MasterId", "MasterName");
        }
    }
}