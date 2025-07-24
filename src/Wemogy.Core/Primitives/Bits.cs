using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Wemogy.Core.Encodings;
using Wemogy.Core.Extensions;
using Wemogy.Core.Primitives.JsonConverters;

namespace Wemogy.Core.Primitives
{
    [JsonConverter(typeof(BitsNewtonsoftJsonConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(BitsJsonConverter))]
    public class Bits
    {
        private readonly List<bool> _state;
        public bool IsWildcard { get; }

        public int Length => _state.Count;

        public bool IsEmpty => Equals(Bits.Empty);

        public Bits(string? base64UrlValue = null)
        {
            if (string.IsNullOrWhiteSpace(base64UrlValue))
            {
                _state = new List<bool>();
                return;
            }

            if (base64UrlValue == "*")
            {
                IsWildcard = true;
                _state = new List<bool>();
                return;
            }

            _state = Base64.DecodeUrl(base64UrlValue!);
        }

        public static Bits Empty
        {
            get
            {
                return new Bits();
            }
        }

        public static Bits Wildcard
        {
            get
            {
                return new Bits("*");
            }
        }

        public override string ToString()
        {
            if (IsWildcard)
            {
                return "*";
            }

            return Base64.EncodeUrl(_state);
        }

        public bool HasFlag(int flagIndex)
        {
            if (IsWildcard)
            {
                return true;
            }

            var listIndex = MapFlagIndexToListIndex(flagIndex);
            if (listIndex == -1)
            {
                return false;
            }

            return _state.ElementAtOrDefault(listIndex);
        }

        public bool HasFlags(IEnumerable<int> flagIndices)
        {
            return flagIndices.All(HasFlag);
        }

        public bool GetFlag(int flagIndex)
        {
            if (IsWildcard)
            {
                return true;
            }

            var listIndex = MapFlagIndexToListIndex(flagIndex);
            if (listIndex == -1)
            {
                return false;
            }

            return _state.ElementAtOrDefault(listIndex);
        }

        public void SetFlag(int flagIndex)
        {
            if (IsWildcard)
            {
                return;
            }

            var listIndex = MapFlagIndexToListIndex(flagIndex);
            if (listIndex == -1)
            {
                return;
            }

            EnsureListIndexExists(listIndex);
            _state[listIndex] = true;
        }

        public void SetFlags(IEnumerable<int> flagIndices)
        {
            flagIndices.ToList().ForEach(SetFlag);
        }

        public void RemoveFlag(int flagIndex)
        {
            if (IsWildcard)
            {
                return;
            }

            var listIndex = MapFlagIndexToListIndex(flagIndex);
            if (listIndex == -1)
            {
                return;
            }

            EnsureListIndexExists(listIndex);
            _state[listIndex] = false;
        }

        public void RemoveFlags(IEnumerable<int> flagIndices)
        {
            flagIndices.ToList().ForEach(RemoveFlag);
        }

        public Bits Or(Bits bits)
        {
            var thisClone = new Bits(ToString());
            if (thisClone.IsWildcard)
            {
                return thisClone;
            }

            if (bits.IsWildcard)
            {
                return bits.Clone();
            }

            var flagIndex = 0;
            foreach (var bit in bits._state)
            {
                flagIndex++;
                if (bit == false)
                {
                    continue;
                }

                thisClone.SetFlag(flagIndex);
            }

            return thisClone;
        }

        public Bits And(Bits bits)
        {
            if (IsWildcard)
            {
                return bits.Clone();
            }

            var thisClone = new Bits(ToString());
            if (bits.IsWildcard)
            {
                return thisClone;
            }

            var maxStateCount = Math.Max(thisClone._state.Count, bits._state.Count);

            EnsureListIndexExists(maxStateCount - 1);
            for (int i = 0; i < maxStateCount; i++)
            {
                var thisValue = thisClone._state.ElementAtOrDefault(i);
                var otherValue = bits._state.ElementAtOrDefault(i);
                var andResult = thisValue & otherValue;
                var flagIndex = i + 1;
                if (andResult)
                {
                    thisClone.SetFlag(flagIndex);
                }
                else
                {
                    thisClone.RemoveFlag(flagIndex);
                }
            }

            return thisClone;
        }

        /// <summary>
        /// The flag index is 1 based, because flagIndex 1 means the first bit
        /// Moreover 0 will be the default if no argument provided, and we don't want to use this default
        /// </summary>
        private int MapFlagIndexToListIndex(int flagIndex)
        {
            return flagIndex - 1;
        }

        private void EnsureListIndexExists(int listIndex)
        {
            // fill the list with default values
            while (_state.Count <= listIndex)
            {
                _state.Add(false);
            }
        }

        public static Bits FromFlag(int flagIndex)
        {
            var bits = new Bits();
            bits.SetFlag(flagIndex);
            return bits;
        }

        public static Bits FromFlags(params Enum[] enums)
        {
            var bits = new Bits();
            var list = new List<Enum>(enums);
            var flagIndices = list.Select(x => (int)(object)x);
            bits.SetFlags(flagIndices);

            return bits;
        }

        public bool Equals(Bits obj)
        {
            // copy
            var thisCopy = new Bits(ToString());
            var objCopy = new Bits(obj.ToString());

            // remove empty flags at the end
            thisCopy.RemoveEmptyFlagsAtTheEnd();
            objCopy.RemoveEmptyFlagsAtTheEnd();

            // compare base64url strings
            return thisCopy.ToString() == objCopy.ToString();
        }

        private void RemoveEmptyFlagsAtTheEnd()
        {
            while ((_state.LastOrDefault() == false) && _state.Any())
            {
                _state.RemoveLast();
            }
        }

        public override bool Equals(object obj)
        {
            return ToString().Equals(obj.ToString());
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// Returns the bits as a binary string (from left to right)
        /// e.g. 1010 (1 is bit 1, 0 is bit 2, 1 is bit 3, 0 is bit 4)
        /// </summary>
        /// <param name="length">The length of the binary string</param>
        public string ToBinaryString(int? length = null)
        {
            length ??= IsWildcard ? 6 : _state.Count;

            var stringBuilder = new StringBuilder();

            for (int i = 1; i <= length; i++)
            {
                stringBuilder.Append(GetFlag(i) ? "1" : "0");
            }

            return stringBuilder.ToString();
        }
    }
}
