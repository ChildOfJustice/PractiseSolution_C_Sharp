using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIRST_CS;
using System.Numerics;

namespace SECOND_CS
{
    

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
            return $"[{Real.ToDouble()} + {Imaginary.ToDouble()}i]";
        }
        public Fraction Absolute(Fraction eps)
        {
            var temp = Real*Real + Imaginary*Imaginary;
            return temp.GetSquareRootNewTon(eps);
        }

        static public Fraction AtanFraction(Fraction x, Fraction eps)
        {
            if(x.Abs() > new Fraction(1, 1))
            {
                throw new Exception("Invalid Argument");
            }
            var sum = new Fraction(0,1);
            var currentValue = new Fraction(0, 1);
            var n = 1;
            do
            {
                currentValue = Fraction.PowFraction(new Fraction(-1, 1), n - 1) * (Fraction.PowFraction(x, 2 * n - 1) / (2 * n - 1));
                sum += currentValue;
                n++;
            } while (currentValue.Abs() > eps);
            return sum;
        }
        static public int Fact(int n)
        {
            var res = 1;
            for (var i = 1; i <= n; i++)
            {
                res = res * i;
            }
            return res;
        }
        static public Fraction SinFraction(Fraction x, Fraction eps)
        {
            var sum = new Fraction(0, 1);
            var currentValue = new Fraction(0, 1);
            var n = 1;
            do
            {
                currentValue = Fraction.PowFraction(new Fraction(-1, 1), n) * (Fraction.PowFraction(x, 2 * n + 1) / Fact(2 * n + 1));
                sum += currentValue;
                n++;
            } while (currentValue.Abs() > eps);
            return sum;
        }
        static public Fraction CosFraction(Fraction x, Fraction eps)
        {
            var sum = new Fraction(0, 1);
            var currentValue = new Fraction(0, 1);
            var n = 1;
            do
            {
                currentValue = Fraction.PowFraction(new Fraction(-1, 1), n) * (Fraction.PowFraction(x, 2 * n) / Fact(2 * n));
                sum += currentValue;
                n++;
            } while (currentValue.Abs() > eps);
            return sum;
        }
        static public Fraction CountPi(Fraction eps)
        {
            var pi = new Fraction(0,1);
            int j = 1;
            Fraction curValue;
            do
            {
                curValue = new Fraction(4, j);
                pi += new Fraction(4, j);
                j += 2;
                curValue = new Fraction(4, j);
                pi -= curValue;
                j += 2;
            } while (curValue.Abs() > eps);
            return pi;
        }

        public Fraction Argument(Fraction eps)
        {
            if(Real.Denominator > 0)
            {
                //разрыв в pi/2, как тогда в ряд раскладывать для использования только Fraction?
                return AtanFraction(Imaginary/ Real, eps);
            } else
            {
                if(Imaginary.Denominator > 0)
                {
                    return CountPi(eps) + AtanFraction((Imaginary / Real), eps);
                }
                return CountPi(eps) + AtanFraction((Imaginary / Real), eps);
            }
        }
        public List<Complex> SqrtN(int n, Fraction eps)
        {
            var double_n = (double)n;
            var power = 1 / double_n;
            var coeff = Math.Pow(this.Absolute(new Fraction(1, 1000)).ToDouble(), power);


            var ret = new List<Complex>();

            for (int i = 0; i < n; i++)
            {
                //Console.WriteLine("added");
                double real_ = coeff * Math.Cos((this.Argument(eps).ToDouble() + 2 * Math.PI * i) / double_n);
                double imaginary_ = coeff * Math.Sin((this.Argument(eps).ToDouble() + 2 * Math.PI * i) / double_n);

                real_ *= 100000.0d;
                imaginary_ *= 100000.0d;
                ret.Add(new Complex(new Fraction((BigInteger)real_, 100000), new Fraction((BigInteger)imaginary_, 100000)));
            }

            return ret;
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
            if (right.Real.Numerator == 0)
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
            return Math.Abs(Real.ToDouble() - cmplx.Real.ToDouble()) < Epsilon
                && Math.Abs(Imaginary.ToDouble() - cmplx.Imaginary.ToDouble()) < Epsilon;
        }

        public bool Equals(int @int)
        {
            return Math.Abs(Real.ToDouble() - @int) < Epsilon
                && Math.Abs(Imaginary.ToDouble()) < Epsilon;
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
