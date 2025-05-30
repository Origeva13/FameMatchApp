﻿using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FameMatchApp.Models
{
    public class User
    {
        public int UserId { get; set; }


        public string UserName { get; set; } = null!;


        public string UserLastName { get; set; } = null!;

        public string UserEmail { get; set; } = null!;


        public string UserPassword { get; set; } = null!;

        public bool IsManager { get; set; }


        public string UserGender { get; set; } = null!;

        public bool IsReported { get; set; }

        public bool IsBlocked { get; set; }

        public virtual Casted? Casted { get; set; }

        public virtual Castor? Castor { get; set; }

        //public virtual ICollection<Message> MessageRecivers { get; set; } = new List<Message>();


        //public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();

        public virtual ICollection<File> Files { get; set; } = new List<File>();
        public User() { }
    }
}
