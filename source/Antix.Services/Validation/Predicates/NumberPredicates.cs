using System;

namespace Antix.Services.Validation.Predicates
{    
	public interface INumberPredicates
    {
 		IValidationPredicate<Decimal> Range(Decimal min, Decimal max);
        IValidationPredicate<Decimal> Max(Decimal max);
		IValidationPredicate<Decimal> Min(Decimal min);
 
 		IValidationPredicate<Double> Range(Double min, Double max);
        IValidationPredicate<Double> Max(Double max);
		IValidationPredicate<Double> Min(Double min);
 
 		IValidationPredicate<Single> Range(Single min, Single max);
        IValidationPredicate<Single> Max(Single max);
		IValidationPredicate<Single> Min(Single min);
 
 		IValidationPredicate<Int16> Range(Int16 min, Int16 max);
        IValidationPredicate<Int16> Max(Int16 max);
		IValidationPredicate<Int16> Min(Int16 min);
 
 		IValidationPredicate<Int32> Range(Int32 min, Int32 max);
        IValidationPredicate<Int32> Max(Int32 max);
		IValidationPredicate<Int32> Min(Int32 min);
 
 		IValidationPredicate<Int64> Range(Int64 min, Int64 max);
        IValidationPredicate<Int64> Max(Int64 max);
		IValidationPredicate<Int64> Min(Int64 min);
 
 		IValidationPredicate<UInt16> Range(UInt16 min, UInt16 max);
        IValidationPredicate<UInt16> Max(UInt16 max);
		IValidationPredicate<UInt16> Min(UInt16 min);
 
 		IValidationPredicate<UInt32> Range(UInt32 min, UInt32 max);
        IValidationPredicate<UInt32> Max(UInt32 max);
		IValidationPredicate<UInt32> Min(UInt32 min);
 
 		IValidationPredicate<UInt64> Range(UInt64 min, UInt64 max);
        IValidationPredicate<UInt64> Max(UInt64 max);
		IValidationPredicate<UInt64> Min(UInt64 min);
 
 		IValidationPredicate<DateTime> Range(DateTime min, DateTime max);
        IValidationPredicate<DateTime> Max(DateTime max);
		IValidationPredicate<DateTime> Min(DateTime min);
 
 		IValidationPredicate<DateTimeOffset> Range(DateTimeOffset min, DateTimeOffset max);
        IValidationPredicate<DateTimeOffset> Max(DateTimeOffset max);
		IValidationPredicate<DateTimeOffset> Min(DateTimeOffset min);
 
 	}

	public partial class StandardValidationPredicates
    {
 		readonly ValidationPredicateCache<DecimalRangePredicate, Tuple<Decimal, Decimal>> _cacheDecimalRange
            = ValidationPredicateCache.Create(
                (Tuple<Decimal, Decimal> k) => new DecimalRangePredicate(k.Item1, k.Item2));
        readonly ValidationPredicateCache<DecimalMinPredicate, Tuple<Decimal>> _cacheDecimalMin
            = ValidationPredicateCache.Create(
                (Tuple<Decimal> k) => new DecimalMinPredicate(k.Item1));
        readonly ValidationPredicateCache<DecimalMaxPredicate, Tuple<Decimal>> _cacheDecimalMax
            = ValidationPredicateCache.Create(
                (Tuple<Decimal> k) => new DecimalMaxPredicate(k.Item1));

		public IValidationPredicate<Decimal> Range(Decimal min, Decimal max)
        {
            return _cacheDecimalRange.GetOrCreate(Tuple.Create(min, max));
        }

        public IValidationPredicate<Decimal> Min(Decimal min)
        {
            return _cacheDecimalMin.GetOrCreate(Tuple.Create(min));
        }

        public IValidationPredicate<Decimal> Max(Decimal max)
        {
            return _cacheDecimalMax.GetOrCreate(Tuple.Create(max));
        }
 
 		readonly ValidationPredicateCache<DoubleRangePredicate, Tuple<Double, Double>> _cacheDoubleRange
            = ValidationPredicateCache.Create(
                (Tuple<Double, Double> k) => new DoubleRangePredicate(k.Item1, k.Item2));
        readonly ValidationPredicateCache<DoubleMinPredicate, Tuple<Double>> _cacheDoubleMin
            = ValidationPredicateCache.Create(
                (Tuple<Double> k) => new DoubleMinPredicate(k.Item1));
        readonly ValidationPredicateCache<DoubleMaxPredicate, Tuple<Double>> _cacheDoubleMax
            = ValidationPredicateCache.Create(
                (Tuple<Double> k) => new DoubleMaxPredicate(k.Item1));

		public IValidationPredicate<Double> Range(Double min, Double max)
        {
            return _cacheDoubleRange.GetOrCreate(Tuple.Create(min, max));
        }

        public IValidationPredicate<Double> Min(Double min)
        {
            return _cacheDoubleMin.GetOrCreate(Tuple.Create(min));
        }

        public IValidationPredicate<Double> Max(Double max)
        {
            return _cacheDoubleMax.GetOrCreate(Tuple.Create(max));
        }
 
 		readonly ValidationPredicateCache<SingleRangePredicate, Tuple<Single, Single>> _cacheSingleRange
            = ValidationPredicateCache.Create(
                (Tuple<Single, Single> k) => new SingleRangePredicate(k.Item1, k.Item2));
        readonly ValidationPredicateCache<SingleMinPredicate, Tuple<Single>> _cacheSingleMin
            = ValidationPredicateCache.Create(
                (Tuple<Single> k) => new SingleMinPredicate(k.Item1));
        readonly ValidationPredicateCache<SingleMaxPredicate, Tuple<Single>> _cacheSingleMax
            = ValidationPredicateCache.Create(
                (Tuple<Single> k) => new SingleMaxPredicate(k.Item1));

		public IValidationPredicate<Single> Range(Single min, Single max)
        {
            return _cacheSingleRange.GetOrCreate(Tuple.Create(min, max));
        }

        public IValidationPredicate<Single> Min(Single min)
        {
            return _cacheSingleMin.GetOrCreate(Tuple.Create(min));
        }

        public IValidationPredicate<Single> Max(Single max)
        {
            return _cacheSingleMax.GetOrCreate(Tuple.Create(max));
        }
 
 		readonly ValidationPredicateCache<Int16RangePredicate, Tuple<Int16, Int16>> _cacheInt16Range
            = ValidationPredicateCache.Create(
                (Tuple<Int16, Int16> k) => new Int16RangePredicate(k.Item1, k.Item2));
        readonly ValidationPredicateCache<Int16MinPredicate, Tuple<Int16>> _cacheInt16Min
            = ValidationPredicateCache.Create(
                (Tuple<Int16> k) => new Int16MinPredicate(k.Item1));
        readonly ValidationPredicateCache<Int16MaxPredicate, Tuple<Int16>> _cacheInt16Max
            = ValidationPredicateCache.Create(
                (Tuple<Int16> k) => new Int16MaxPredicate(k.Item1));

		public IValidationPredicate<Int16> Range(Int16 min, Int16 max)
        {
            return _cacheInt16Range.GetOrCreate(Tuple.Create(min, max));
        }

        public IValidationPredicate<Int16> Min(Int16 min)
        {
            return _cacheInt16Min.GetOrCreate(Tuple.Create(min));
        }

        public IValidationPredicate<Int16> Max(Int16 max)
        {
            return _cacheInt16Max.GetOrCreate(Tuple.Create(max));
        }
 
 		readonly ValidationPredicateCache<Int32RangePredicate, Tuple<Int32, Int32>> _cacheInt32Range
            = ValidationPredicateCache.Create(
                (Tuple<Int32, Int32> k) => new Int32RangePredicate(k.Item1, k.Item2));
        readonly ValidationPredicateCache<Int32MinPredicate, Tuple<Int32>> _cacheInt32Min
            = ValidationPredicateCache.Create(
                (Tuple<Int32> k) => new Int32MinPredicate(k.Item1));
        readonly ValidationPredicateCache<Int32MaxPredicate, Tuple<Int32>> _cacheInt32Max
            = ValidationPredicateCache.Create(
                (Tuple<Int32> k) => new Int32MaxPredicate(k.Item1));

		public IValidationPredicate<Int32> Range(Int32 min, Int32 max)
        {
            return _cacheInt32Range.GetOrCreate(Tuple.Create(min, max));
        }

        public IValidationPredicate<Int32> Min(Int32 min)
        {
            return _cacheInt32Min.GetOrCreate(Tuple.Create(min));
        }

        public IValidationPredicate<Int32> Max(Int32 max)
        {
            return _cacheInt32Max.GetOrCreate(Tuple.Create(max));
        }
 
 		readonly ValidationPredicateCache<Int64RangePredicate, Tuple<Int64, Int64>> _cacheInt64Range
            = ValidationPredicateCache.Create(
                (Tuple<Int64, Int64> k) => new Int64RangePredicate(k.Item1, k.Item2));
        readonly ValidationPredicateCache<Int64MinPredicate, Tuple<Int64>> _cacheInt64Min
            = ValidationPredicateCache.Create(
                (Tuple<Int64> k) => new Int64MinPredicate(k.Item1));
        readonly ValidationPredicateCache<Int64MaxPredicate, Tuple<Int64>> _cacheInt64Max
            = ValidationPredicateCache.Create(
                (Tuple<Int64> k) => new Int64MaxPredicate(k.Item1));

		public IValidationPredicate<Int64> Range(Int64 min, Int64 max)
        {
            return _cacheInt64Range.GetOrCreate(Tuple.Create(min, max));
        }

        public IValidationPredicate<Int64> Min(Int64 min)
        {
            return _cacheInt64Min.GetOrCreate(Tuple.Create(min));
        }

        public IValidationPredicate<Int64> Max(Int64 max)
        {
            return _cacheInt64Max.GetOrCreate(Tuple.Create(max));
        }
 
 		readonly ValidationPredicateCache<UInt16RangePredicate, Tuple<UInt16, UInt16>> _cacheUInt16Range
            = ValidationPredicateCache.Create(
                (Tuple<UInt16, UInt16> k) => new UInt16RangePredicate(k.Item1, k.Item2));
        readonly ValidationPredicateCache<UInt16MinPredicate, Tuple<UInt16>> _cacheUInt16Min
            = ValidationPredicateCache.Create(
                (Tuple<UInt16> k) => new UInt16MinPredicate(k.Item1));
        readonly ValidationPredicateCache<UInt16MaxPredicate, Tuple<UInt16>> _cacheUInt16Max
            = ValidationPredicateCache.Create(
                (Tuple<UInt16> k) => new UInt16MaxPredicate(k.Item1));

		public IValidationPredicate<UInt16> Range(UInt16 min, UInt16 max)
        {
            return _cacheUInt16Range.GetOrCreate(Tuple.Create(min, max));
        }

        public IValidationPredicate<UInt16> Min(UInt16 min)
        {
            return _cacheUInt16Min.GetOrCreate(Tuple.Create(min));
        }

        public IValidationPredicate<UInt16> Max(UInt16 max)
        {
            return _cacheUInt16Max.GetOrCreate(Tuple.Create(max));
        }
 
 		readonly ValidationPredicateCache<UInt32RangePredicate, Tuple<UInt32, UInt32>> _cacheUInt32Range
            = ValidationPredicateCache.Create(
                (Tuple<UInt32, UInt32> k) => new UInt32RangePredicate(k.Item1, k.Item2));
        readonly ValidationPredicateCache<UInt32MinPredicate, Tuple<UInt32>> _cacheUInt32Min
            = ValidationPredicateCache.Create(
                (Tuple<UInt32> k) => new UInt32MinPredicate(k.Item1));
        readonly ValidationPredicateCache<UInt32MaxPredicate, Tuple<UInt32>> _cacheUInt32Max
            = ValidationPredicateCache.Create(
                (Tuple<UInt32> k) => new UInt32MaxPredicate(k.Item1));

		public IValidationPredicate<UInt32> Range(UInt32 min, UInt32 max)
        {
            return _cacheUInt32Range.GetOrCreate(Tuple.Create(min, max));
        }

        public IValidationPredicate<UInt32> Min(UInt32 min)
        {
            return _cacheUInt32Min.GetOrCreate(Tuple.Create(min));
        }

        public IValidationPredicate<UInt32> Max(UInt32 max)
        {
            return _cacheUInt32Max.GetOrCreate(Tuple.Create(max));
        }
 
 		readonly ValidationPredicateCache<UInt64RangePredicate, Tuple<UInt64, UInt64>> _cacheUInt64Range
            = ValidationPredicateCache.Create(
                (Tuple<UInt64, UInt64> k) => new UInt64RangePredicate(k.Item1, k.Item2));
        readonly ValidationPredicateCache<UInt64MinPredicate, Tuple<UInt64>> _cacheUInt64Min
            = ValidationPredicateCache.Create(
                (Tuple<UInt64> k) => new UInt64MinPredicate(k.Item1));
        readonly ValidationPredicateCache<UInt64MaxPredicate, Tuple<UInt64>> _cacheUInt64Max
            = ValidationPredicateCache.Create(
                (Tuple<UInt64> k) => new UInt64MaxPredicate(k.Item1));

		public IValidationPredicate<UInt64> Range(UInt64 min, UInt64 max)
        {
            return _cacheUInt64Range.GetOrCreate(Tuple.Create(min, max));
        }

        public IValidationPredicate<UInt64> Min(UInt64 min)
        {
            return _cacheUInt64Min.GetOrCreate(Tuple.Create(min));
        }

        public IValidationPredicate<UInt64> Max(UInt64 max)
        {
            return _cacheUInt64Max.GetOrCreate(Tuple.Create(max));
        }
 
 		readonly ValidationPredicateCache<DateTimeRangePredicate, Tuple<DateTime, DateTime>> _cacheDateTimeRange
            = ValidationPredicateCache.Create(
                (Tuple<DateTime, DateTime> k) => new DateTimeRangePredicate(k.Item1, k.Item2));
        readonly ValidationPredicateCache<DateTimeMinPredicate, Tuple<DateTime>> _cacheDateTimeMin
            = ValidationPredicateCache.Create(
                (Tuple<DateTime> k) => new DateTimeMinPredicate(k.Item1));
        readonly ValidationPredicateCache<DateTimeMaxPredicate, Tuple<DateTime>> _cacheDateTimeMax
            = ValidationPredicateCache.Create(
                (Tuple<DateTime> k) => new DateTimeMaxPredicate(k.Item1));

		public IValidationPredicate<DateTime> Range(DateTime min, DateTime max)
        {
            return _cacheDateTimeRange.GetOrCreate(Tuple.Create(min, max));
        }

        public IValidationPredicate<DateTime> Min(DateTime min)
        {
            return _cacheDateTimeMin.GetOrCreate(Tuple.Create(min));
        }

        public IValidationPredicate<DateTime> Max(DateTime max)
        {
            return _cacheDateTimeMax.GetOrCreate(Tuple.Create(max));
        }
 
 		readonly ValidationPredicateCache<DateTimeOffsetRangePredicate, Tuple<DateTimeOffset, DateTimeOffset>> _cacheDateTimeOffsetRange
            = ValidationPredicateCache.Create(
                (Tuple<DateTimeOffset, DateTimeOffset> k) => new DateTimeOffsetRangePredicate(k.Item1, k.Item2));
        readonly ValidationPredicateCache<DateTimeOffsetMinPredicate, Tuple<DateTimeOffset>> _cacheDateTimeOffsetMin
            = ValidationPredicateCache.Create(
                (Tuple<DateTimeOffset> k) => new DateTimeOffsetMinPredicate(k.Item1));
        readonly ValidationPredicateCache<DateTimeOffsetMaxPredicate, Tuple<DateTimeOffset>> _cacheDateTimeOffsetMax
            = ValidationPredicateCache.Create(
                (Tuple<DateTimeOffset> k) => new DateTimeOffsetMaxPredicate(k.Item1));

		public IValidationPredicate<DateTimeOffset> Range(DateTimeOffset min, DateTimeOffset max)
        {
            return _cacheDateTimeOffsetRange.GetOrCreate(Tuple.Create(min, max));
        }

        public IValidationPredicate<DateTimeOffset> Min(DateTimeOffset min)
        {
            return _cacheDateTimeOffsetMin.GetOrCreate(Tuple.Create(min));
        }

        public IValidationPredicate<DateTimeOffset> Max(DateTimeOffset max)
        {
            return _cacheDateTimeOffsetMax.GetOrCreate(Tuple.Create(max));
        }
 
 	}

    
    public class DecimalMaxPredicate :
        ValidationPredicateBase<Decimal>
    {
        readonly Decimal _max;

        public DecimalMaxPredicate(Decimal max) :
			base("number-max")
        {
            _max = max;
        }

        public override bool Is(Decimal model)
        {
            return model <= _max;
        }

        public override string ToString()
        {
            return NameFormat("max", _max);
        }
    }

    public class DecimalMinPredicate :
        ValidationPredicateBase<Decimal>
    {
        readonly Decimal _min;

        public DecimalMinPredicate(Decimal min) :
			base("number-min")
        {
            _min = min;
        }

        public override bool Is(Decimal model)
        {
            return model >= _min;
        }

        public override string ToString()
        {
            return NameFormat("min", _min);
        }
    }

    public class DecimalRangePredicate :
        ValidationPredicateBase<Decimal>
    {
        readonly Decimal _min;
        readonly Decimal _max;

        public DecimalRangePredicate(Decimal min, Decimal max) :
			base("number-range")
        {
            _min = min;
            _max = max;
        }

        public override bool Is(Decimal model)
        {
            return model <= _min
					&& model >= _max;
        }

        public override string ToString()
        {
            return NameFormat("min", _min, "max", _max);
        }
    }

    public class DoubleMaxPredicate :
        ValidationPredicateBase<Double>
    {
        readonly Double _max;

        public DoubleMaxPredicate(Double max) :
			base("number-max")
        {
            _max = max;
        }

        public override bool Is(Double model)
        {
            return model <= _max;
        }

        public override string ToString()
        {
            return NameFormat("max", _max);
        }
    }

    public class DoubleMinPredicate :
        ValidationPredicateBase<Double>
    {
        readonly Double _min;

        public DoubleMinPredicate(Double min) :
			base("number-min")
        {
            _min = min;
        }

        public override bool Is(Double model)
        {
            return model >= _min;
        }

        public override string ToString()
        {
            return NameFormat("min", _min);
        }
    }

    public class DoubleRangePredicate :
        ValidationPredicateBase<Double>
    {
        readonly Double _min;
        readonly Double _max;

        public DoubleRangePredicate(Double min, Double max) :
			base("number-range")
        {
            _min = min;
            _max = max;
        }

        public override bool Is(Double model)
        {
            return model <= _min
					&& model >= _max;
        }

        public override string ToString()
        {
            return NameFormat("min", _min, "max", _max);
        }
    }

    public class SingleMaxPredicate :
        ValidationPredicateBase<Single>
    {
        readonly Single _max;

        public SingleMaxPredicate(Single max) :
			base("number-max")
        {
            _max = max;
        }

        public override bool Is(Single model)
        {
            return model <= _max;
        }

        public override string ToString()
        {
            return NameFormat("max", _max);
        }
    }

    public class SingleMinPredicate :
        ValidationPredicateBase<Single>
    {
        readonly Single _min;

        public SingleMinPredicate(Single min) :
			base("number-min")
        {
            _min = min;
        }

        public override bool Is(Single model)
        {
            return model >= _min;
        }

        public override string ToString()
        {
            return NameFormat("min", _min);
        }
    }

    public class SingleRangePredicate :
        ValidationPredicateBase<Single>
    {
        readonly Single _min;
        readonly Single _max;

        public SingleRangePredicate(Single min, Single max) :
			base("number-range")
        {
            _min = min;
            _max = max;
        }

        public override bool Is(Single model)
        {
            return model <= _min
					&& model >= _max;
        }

        public override string ToString()
        {
            return NameFormat("min", _min, "max", _max);
        }
    }

    public class Int16MaxPredicate :
        ValidationPredicateBase<Int16>
    {
        readonly Int16 _max;

        public Int16MaxPredicate(Int16 max) :
			base("number-max")
        {
            _max = max;
        }

        public override bool Is(Int16 model)
        {
            return model <= _max;
        }

        public override string ToString()
        {
            return NameFormat("max", _max);
        }
    }

    public class Int16MinPredicate :
        ValidationPredicateBase<Int16>
    {
        readonly Int16 _min;

        public Int16MinPredicate(Int16 min) :
			base("number-min")
        {
            _min = min;
        }

        public override bool Is(Int16 model)
        {
            return model >= _min;
        }

        public override string ToString()
        {
            return NameFormat("min", _min);
        }
    }

    public class Int16RangePredicate :
        ValidationPredicateBase<Int16>
    {
        readonly Int16 _min;
        readonly Int16 _max;

        public Int16RangePredicate(Int16 min, Int16 max) :
			base("number-range")
        {
            _min = min;
            _max = max;
        }

        public override bool Is(Int16 model)
        {
            return model <= _min
					&& model >= _max;
        }

        public override string ToString()
        {
            return NameFormat("min", _min, "max", _max);
        }
    }

    public class Int32MaxPredicate :
        ValidationPredicateBase<Int32>
    {
        readonly Int32 _max;

        public Int32MaxPredicate(Int32 max) :
			base("number-max")
        {
            _max = max;
        }

        public override bool Is(Int32 model)
        {
            return model <= _max;
        }

        public override string ToString()
        {
            return NameFormat("max", _max);
        }
    }

    public class Int32MinPredicate :
        ValidationPredicateBase<Int32>
    {
        readonly Int32 _min;

        public Int32MinPredicate(Int32 min) :
			base("number-min")
        {
            _min = min;
        }

        public override bool Is(Int32 model)
        {
            return model >= _min;
        }

        public override string ToString()
        {
            return NameFormat("min", _min);
        }
    }

    public class Int32RangePredicate :
        ValidationPredicateBase<Int32>
    {
        readonly Int32 _min;
        readonly Int32 _max;

        public Int32RangePredicate(Int32 min, Int32 max) :
			base("number-range")
        {
            _min = min;
            _max = max;
        }

        public override bool Is(Int32 model)
        {
            return model <= _min
					&& model >= _max;
        }

        public override string ToString()
        {
            return NameFormat("min", _min, "max", _max);
        }
    }

    public class Int64MaxPredicate :
        ValidationPredicateBase<Int64>
    {
        readonly Int64 _max;

        public Int64MaxPredicate(Int64 max) :
			base("number-max")
        {
            _max = max;
        }

        public override bool Is(Int64 model)
        {
            return model <= _max;
        }

        public override string ToString()
        {
            return NameFormat("max", _max);
        }
    }

    public class Int64MinPredicate :
        ValidationPredicateBase<Int64>
    {
        readonly Int64 _min;

        public Int64MinPredicate(Int64 min) :
			base("number-min")
        {
            _min = min;
        }

        public override bool Is(Int64 model)
        {
            return model >= _min;
        }

        public override string ToString()
        {
            return NameFormat("min", _min);
        }
    }

    public class Int64RangePredicate :
        ValidationPredicateBase<Int64>
    {
        readonly Int64 _min;
        readonly Int64 _max;

        public Int64RangePredicate(Int64 min, Int64 max) :
			base("number-range")
        {
            _min = min;
            _max = max;
        }

        public override bool Is(Int64 model)
        {
            return model <= _min
					&& model >= _max;
        }

        public override string ToString()
        {
            return NameFormat("min", _min, "max", _max);
        }
    }

    public class UInt16MaxPredicate :
        ValidationPredicateBase<UInt16>
    {
        readonly UInt16 _max;

        public UInt16MaxPredicate(UInt16 max) :
			base("number-max")
        {
            _max = max;
        }

        public override bool Is(UInt16 model)
        {
            return model <= _max;
        }

        public override string ToString()
        {
            return NameFormat("max", _max);
        }
    }

    public class UInt16MinPredicate :
        ValidationPredicateBase<UInt16>
    {
        readonly UInt16 _min;

        public UInt16MinPredicate(UInt16 min) :
			base("number-min")
        {
            _min = min;
        }

        public override bool Is(UInt16 model)
        {
            return model >= _min;
        }

        public override string ToString()
        {
            return NameFormat("min", _min);
        }
    }

    public class UInt16RangePredicate :
        ValidationPredicateBase<UInt16>
    {
        readonly UInt16 _min;
        readonly UInt16 _max;

        public UInt16RangePredicate(UInt16 min, UInt16 max) :
			base("number-range")
        {
            _min = min;
            _max = max;
        }

        public override bool Is(UInt16 model)
        {
            return model <= _min
					&& model >= _max;
        }

        public override string ToString()
        {
            return NameFormat("min", _min, "max", _max);
        }
    }

    public class UInt32MaxPredicate :
        ValidationPredicateBase<UInt32>
    {
        readonly UInt32 _max;

        public UInt32MaxPredicate(UInt32 max) :
			base("number-max")
        {
            _max = max;
        }

        public override bool Is(UInt32 model)
        {
            return model <= _max;
        }

        public override string ToString()
        {
            return NameFormat("max", _max);
        }
    }

    public class UInt32MinPredicate :
        ValidationPredicateBase<UInt32>
    {
        readonly UInt32 _min;

        public UInt32MinPredicate(UInt32 min) :
			base("number-min")
        {
            _min = min;
        }

        public override bool Is(UInt32 model)
        {
            return model >= _min;
        }

        public override string ToString()
        {
            return NameFormat("min", _min);
        }
    }

    public class UInt32RangePredicate :
        ValidationPredicateBase<UInt32>
    {
        readonly UInt32 _min;
        readonly UInt32 _max;

        public UInt32RangePredicate(UInt32 min, UInt32 max) :
			base("number-range")
        {
            _min = min;
            _max = max;
        }

        public override bool Is(UInt32 model)
        {
            return model <= _min
					&& model >= _max;
        }

        public override string ToString()
        {
            return NameFormat("min", _min, "max", _max);
        }
    }

    public class UInt64MaxPredicate :
        ValidationPredicateBase<UInt64>
    {
        readonly UInt64 _max;

        public UInt64MaxPredicate(UInt64 max) :
			base("number-max")
        {
            _max = max;
        }

        public override bool Is(UInt64 model)
        {
            return model <= _max;
        }

        public override string ToString()
        {
            return NameFormat("max", _max);
        }
    }

    public class UInt64MinPredicate :
        ValidationPredicateBase<UInt64>
    {
        readonly UInt64 _min;

        public UInt64MinPredicate(UInt64 min) :
			base("number-min")
        {
            _min = min;
        }

        public override bool Is(UInt64 model)
        {
            return model >= _min;
        }

        public override string ToString()
        {
            return NameFormat("min", _min);
        }
    }

    public class UInt64RangePredicate :
        ValidationPredicateBase<UInt64>
    {
        readonly UInt64 _min;
        readonly UInt64 _max;

        public UInt64RangePredicate(UInt64 min, UInt64 max) :
			base("number-range")
        {
            _min = min;
            _max = max;
        }

        public override bool Is(UInt64 model)
        {
            return model <= _min
					&& model >= _max;
        }

        public override string ToString()
        {
            return NameFormat("min", _min, "max", _max);
        }
    }

    public class DateTimeMaxPredicate :
        ValidationPredicateBase<DateTime>
    {
        readonly DateTime _max;

        public DateTimeMaxPredicate(DateTime max) :
			base("number-max")
        {
            _max = max;
        }

        public override bool Is(DateTime model)
        {
            return model <= _max;
        }

        public override string ToString()
        {
            return NameFormat("max", _max);
        }
    }

    public class DateTimeMinPredicate :
        ValidationPredicateBase<DateTime>
    {
        readonly DateTime _min;

        public DateTimeMinPredicate(DateTime min) :
			base("number-min")
        {
            _min = min;
        }

        public override bool Is(DateTime model)
        {
            return model >= _min;
        }

        public override string ToString()
        {
            return NameFormat("min", _min);
        }
    }

    public class DateTimeRangePredicate :
        ValidationPredicateBase<DateTime>
    {
        readonly DateTime _min;
        readonly DateTime _max;

        public DateTimeRangePredicate(DateTime min, DateTime max) :
			base("number-range")
        {
            _min = min;
            _max = max;
        }

        public override bool Is(DateTime model)
        {
            return model <= _min
					&& model >= _max;
        }

        public override string ToString()
        {
            return NameFormat("min", _min, "max", _max);
        }
    }

    public class DateTimeOffsetMaxPredicate :
        ValidationPredicateBase<DateTimeOffset>
    {
        readonly DateTimeOffset _max;

        public DateTimeOffsetMaxPredicate(DateTimeOffset max) :
			base("number-max")
        {
            _max = max;
        }

        public override bool Is(DateTimeOffset model)
        {
            return model <= _max;
        }

        public override string ToString()
        {
            return NameFormat("max", _max);
        }
    }

    public class DateTimeOffsetMinPredicate :
        ValidationPredicateBase<DateTimeOffset>
    {
        readonly DateTimeOffset _min;

        public DateTimeOffsetMinPredicate(DateTimeOffset min) :
			base("number-min")
        {
            _min = min;
        }

        public override bool Is(DateTimeOffset model)
        {
            return model >= _min;
        }

        public override string ToString()
        {
            return NameFormat("min", _min);
        }
    }

    public class DateTimeOffsetRangePredicate :
        ValidationPredicateBase<DateTimeOffset>
    {
        readonly DateTimeOffset _min;
        readonly DateTimeOffset _max;

        public DateTimeOffsetRangePredicate(DateTimeOffset min, DateTimeOffset max) :
			base("number-range")
        {
            _min = min;
            _max = max;
        }

        public override bool Is(DateTimeOffset model)
        {
            return model <= _min
					&& model >= _max;
        }

        public override string ToString()
        {
            return NameFormat("min", _min, "max", _max);
        }
    }
}