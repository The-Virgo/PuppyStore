using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PuppyStoreFinal.Models
{
    public class Puppy
    {
        /// <summary>
        /// The auto assigned id of a puppy
        /// </summary>
        [Key]
        public int PuppyId { get; set; }

        /// <summary>
        /// The nickname given to a puppy until it finds its home
        /// </summary>
        public string Nickname { get; set; }


        /// <summary>
        /// The breed of the puppy
        /// </summary>
        public string Breed { get; set; }

        /// <summary>
        /// The day the puppy was born (day and month)
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// The age of the puppy in weeks
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// The sex of the puppy - false = male, true = female
        /// </summary>
        public bool Sex { get; set; }

        /// <summary>
        /// Whether or not it's a puppy that has already been sold
        /// </summary>
        public bool Sold { get; set; }

    }
}

