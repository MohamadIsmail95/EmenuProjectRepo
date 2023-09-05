using EmenuDAL.EmenuDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmenuDAL.Model.Seeder
{
    public class SeederData
    {
        private readonly EmenuAppDbContext _context;

        public SeederData(EmenuAppDbContext contect)
        {
            _context=contect;
        }

        public void Seed()
        {
            if(!_context.Attributes.Any())
            {
                var attributes = new List<Attribute>()
                {
                    new Attribute()
                    {
                        Name="Color",
                        Description="Color"
                    },
                     new Attribute()
                    {
                        Name="Size",
                        Description="Size"
                    }
                };

                _context.Attributes.AddRange(attributes);
                _context.SaveChanges();
            }

            if (!_context.Variants.Any())
            {
                var variants = new List<Variant>()
                {
                    new Variant()
                    {
                        Name="Red",
                        Description="Red",
                        AttributeId=1
                    },
                    new Variant()
                    {
                        Name="Green",
                        Description="Green",
                        AttributeId=1
                    },
                    new Variant()
                    {
                        Name="Blue",
                        Description="Blue",
                        AttributeId=1
                    },

                    new Variant()
                    {
                        Name="Large",
                        Description="Large",
                        AttributeId=2
                    },
                    new Variant()
                    {
                        Name="XXL Large",
                        Description="Large",
                        AttributeId=2
                    }

                };

                _context.Variants.AddRange(variants);
                _context.SaveChanges();
            }


        }
    }
}
