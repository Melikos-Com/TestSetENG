using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public class Includes<T>
    {
        public List<Expression<Func<T, object>>> includes;


        public Includes()
        {
            this.includes = new List<Expression<Func<T, object>>>();
        }


        public void Add( Expression<Func<T, object>> exp)
          => this.includes.Add(exp);
    
        public void Clear()
            => this.includes = new List<Expression<Func<T, object>>>();

        public List<Expression<Func<T, object>>> Get()
            => this.includes ?? new List<Expression<Func<T, object>>>();


    }
}
