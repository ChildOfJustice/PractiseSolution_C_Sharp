using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiseSolution
{
    abstract class Figure
    {


        private string _name;


        public Figure(string name)
        {
            Name = name;
        }

        public virtual void Example()
        {
            //no need to override
        }

        public abstract double Square();

        public string Name
        {
            get => _name;

            set
            {
                _name = value;
                //bla bla bla
            }
        }

        public object AutoProp
        {
            get;
            private set;
        } = "123";

        public override string ToString()
        {
            return $"Figure {Name}, square = {Square()}";
        }
    }

    internal class Circle : Figure
    {
        public Circle(string name): base(name)
        {

        }

        public override double Square()
        {
            throw new NotImplementedException();
        }



    }
}
