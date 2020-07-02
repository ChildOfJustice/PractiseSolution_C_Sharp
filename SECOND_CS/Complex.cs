using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIRST_CS;
using System.Numerics;

namespace SECOND_CS
{
    /*
     * Реализуйте
        нахождение модуля и аргумента, возведение в степень, а также извлечение
        корня n-ной степени
    */

    class Complex : IEquatable<Complex>,
        IComparable, IComparable<Complex>,
        ICloneable, IEquatable<int>
    {
        Fraction _real;
        Fraction _imaginary;

        private const double Epsilon = 1e-10;

        public Complex(Fraction real, Fraction imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public override string ToString()
        {
            return $"[{Real.GetRoundedValue()} + {Imaginary.GetRoundedValue()}i]";
        }
        public Fraction Absolute(Fraction eps)
        {
            var temp = Real*Real + Imaginary*Imaginary;
            return temp.getSquareRootNewTon(eps);
        }
        public double Argument()
        {
            if(Real.Sign > 0)
            {
                return Math.Atan((Imaginary / Real).GetRoundedValue());
            } else
            {
                if(Imaginary.Sign > 0)
                {
                    return Math.PI + Math.Atan((Imaginary / Real).GetRoundedValue());
                }
                return -Math.PI + Math.Atan((Imaginary / Real).GetRoundedValue());
            }
        }
        public Complex SqrtN(int n)
        {
            var double_n = (double)n;
            var power = 1/double_n;
            var coeff = Math.Pow(this.Absolute(new Fraction(1, 1000)).GetRoundedValue(), power);

            double real_ = coeff*Math.Cos((this.Argument()+ 2*Math.PI) / double_n);
            double imaginary_ = coeff * Math.Sin((this.Argument() + 2 * Math.PI) / double_n);

            real_ *= 100000.0d;
            imaginary_ *= 100000.0d;

            return new Complex(new Fraction((BigInteger)real_,100000),new Fraction((BigInteger)imaginary_,100000));
        }

        public Fraction Real
        {
            get => _real;

            private set => _real = value;
        }
        public Fraction Imaginary
        {
            get => _imaginary;

            private set => _imaginary = value;
        }

        #region Operators
        public static Complex Addition(Complex left, Complex right)
        {
            Complex newComplex = new Complex(left.Real, left.Imaginary);
            newComplex.Real += right.Real;
            newComplex.Imaginary += right.Imaginary;

            return newComplex;
        }
        public static Complex operator +(Complex left, Complex right)
        {
            return Addition(left, right);
        }

        public static Complex Subtraction(Complex left, Complex right)
        {
            Complex newComplex = new Complex(left.Real, left.Imaginary);
            newComplex.Real -= right.Real;
            newComplex.Imaginary -= right.Imaginary;

            return newComplex;
        }
        public static Complex operator -(Complex left, Complex right)
        {
            return Subtraction(left, right);
        }

        public static Complex Multiplication(Complex left, Complex right)
        {
            Complex newComplex = new Complex(left.Real* right.Real - left.Imaginary * right.Imaginary, left.Real*right.Imaginary + right.Real* left.Imaginary);
            return newComplex;
        }
        public static Complex operator *(Complex left, Complex right)
        {
            return Multiplication(left, right);
        }

        public static Complex Division(Complex left, Complex right)
        {
            if (right.Real.IsZero())
            {
                throw new DivideByZeroException("В знаменателе не может быть нуля");
            }

            Complex newComplex = new Complex((left.Real*right.Real + left.Imaginary*right.Imaginary)/(right.Real*right.Real + right.Imaginary*right.Imaginary), (left.Imaginary * right.Real - left.Real * right.Imaginary) / (right.Real * right.Real + right.Imaginary * right.Imaginary));

            return newComplex;
        }
        public static Complex operator /(Complex left, Complex right)
        {
            return Division(left, right);
        }

        #endregion

        #region Equals

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (obj is Complex cmplx)
            {
                return Equals(cmplx);
            }
            if (obj is int @int)
            {
                return Equals(@int);
            }
            return false;
        }

        public bool Equals(Complex cmplx)
        {
            if (cmplx is null)
            {
                return false;
            }
            return Math.Abs(Real.GetRoundedValue() - cmplx.Real.GetRoundedValue()) < Epsilon
                && Math.Abs(Imaginary.GetRoundedValue() - cmplx.Imaginary.GetRoundedValue()) < Epsilon;
        }

        public bool Equals(int @int)
        {
            return Math.Abs(Real.GetRoundedValue() - @int) < Epsilon
                && Math.Abs(Imaginary.GetRoundedValue()) < Epsilon;
        }

        #endregion

        #region CompareTo

        public int CompareTo(object obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            if (obj is Complex cmplx)
            {
                return CompareTo(cmplx);
            }
            throw new ArgumentException("Invalid type", nameof(obj));
        }

        public int CompareTo(Complex cmplx)
        {
            if (cmplx is null)
            {
                throw new ArgumentNullException(nameof(cmplx));
            }
            var realComparisonResult = Real.CompareTo(cmplx.Real);
            if (realComparisonResult == 0)
            {
                var imaginaryComparisonResult = Imaginary.CompareTo(
                    cmplx.Imaginary);
                return imaginaryComparisonResult;
            }
            return realComparisonResult;
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            return new Complex(Real, Imaginary);
        }

        #endregion
    }
}
