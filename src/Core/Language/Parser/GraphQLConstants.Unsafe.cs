using System.Runtime.CompilerServices;

namespace HotChocolate.Language
{
    /// <summary>
    /// This class provides internal char utilities
    /// that are used to tokenize a GraphQL source text.
    /// These utilities are used by the lexer dfault implementation.
    /// </summary>
    internal static partial class GraphQLConstants
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool IsLetterOrDigitOrUnderscoreUnsafe(this byte c)
        {
            fixed (bool* p = _isLetterOrDigitOrUnderscore)
            {
                return p[c];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool IsLetterOrUnderscoreUnsafe(this byte c)
        {
            fixed (bool* p = _isLetterOrUnderscore)
            {
                return p[c];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLetterOrUnderscore(this char c) =>
            IsLetterOrUnderscore((byte)c);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool IsDigit(this byte c)
        {
            fixed (bool* p = _isDigit)
            {
                return p[c];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool IsDigitOrMinus(this byte c)
        {
            fixed (bool* p = _isDigitOrMinus)
            {
                return p[c];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool IsPunctuator(this byte c)
        {
            fixed (bool* p = _isPunctuator)
            {
                return p[c];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidEscapeCharacter(this byte c)
        {
            return _isEscapeCharacter[c];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte EscapeCharacter(this byte c)
        {
            return _escapeCharacters[c];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsControlCharacterNoNewLine(this byte c)
        {
            return _isControlCharacterNoNewLine[c];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TrimComment(this byte c)
        {
            return _trimComment[c];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TokenKind PunctuatorKind(this byte c)
        {
            return _punctuatorKind[c];
        }
    }
}
