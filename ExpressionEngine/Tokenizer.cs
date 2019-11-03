using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace ExpressionEngine
{
    public class Tokenizer
    {
        private readonly Dictionary<Token, Token[]> AllowedPrevTokenMap;
        private readonly Token[] Operations = new[] {Token.Add, Token.Subtract, Token.Multiply, Token.Divide};
        private readonly Token[] AllowedBeforeOperations = new[] {Token.CloseParens, Token.Identifier, Token.Number};

        public Tokenizer(TextReader reader)
        {
            var all = Enum.GetValues(typeof(Token)).Cast<Token>().ToArray();
            AllowedPrevTokenMap = new Dictionary<Token, Token[]>() {
                [Token.EOF] = all,
                [Token.Add] = AllowedBeforeOperations,
                [Token.Subtract] = AllowedBeforeOperations.Concat(new[] {Token.OpenParens}).ToArray(),
                [Token.Multiply] = AllowedBeforeOperations,
                [Token.Divide] = AllowedBeforeOperations,
                [Token.OpenParens] = Operations.Concat(new[] {Token.OpenParens, Token.Identifier, Token.Number}).ToArray(),
                [Token.CloseParens] = new[] {Token.Identifier, Token.Number},
                [Token.Comma] = new[] {Token.Identifier, Token.Number},
                [Token.Identifier] = Operations.Concat(new[] {Token.Identifier, Token.Number, Token.OpenParens}).ToArray(),
                [Token.Number] = Operations.Concat(new[] {Token.Comma, Token.Identifier, Token.Number, Token.OpenParens}).ToArray(),
            };
            _reader = reader;
            NextChar();
            NextToken();
        }

        TextReader _reader;
        char _currentChar;
        Token _currentToken;
        Token _prevToken;
        double _number;
        string _identifier;

        public Token PrevToken {
            get { return _prevToken; }
        }

        public Token Token {
            get { return _currentToken; }
            private set {
                _prevToken = _currentToken;
                _currentToken = value;
            }
        }

        public double Number {
            get { return _number; }
        }

        public string Identifier {
            get { return _identifier; }
        }

        // Read the next character from the input strem
        // and store it in _currentChar, or load '\0' if EOF
        void NextChar()
        {
            int ch = _reader.Read();
            _currentChar = ch < 0 ? '\0' : (char) ch;
        }

        // Read the next token from the input stream
        public void NextToken()
        {
            // Skip whitespace
            while (char.IsWhiteSpace(_currentChar)) {
                NextChar();
            }

            // Special characters
            switch (_currentChar) {
                case '\0':
                    Token = Token.EOF;
                    CheckPrevToken();
                    return;

                case '+':
                    NextChar();
                    Token = Token.Add;
                    CheckPrevToken();
                    return;

                case '-':
                    NextChar();
                    Token = Token.Subtract;
                    CheckPrevToken();
                    return;

                case '*':
                    NextChar();
                    Token = Token.Multiply;
                    CheckPrevToken();
                    return;

                case '/':
                    NextChar();
                    Token = Token.Divide;
                    CheckPrevToken();
                    return;

                case '(':
                    NextChar();
                    Token = Token.OpenParens;
                    CheckPrevToken();
                    return;

                case ')':
                    NextChar();
                    Token = Token.CloseParens;
                    CheckPrevToken();
                    return;

                case ',':
                    NextChar();
                    Token = Token.Comma;
                    CheckPrevToken();
                    return;
            }

            // Number?
            if (char.IsDigit(_currentChar) || _currentChar == '.') {
                // Capture digits/decimal point
                var sb = new StringBuilder();
                bool haveDecimalPoint = false;
                while (char.IsDigit(_currentChar) || (!haveDecimalPoint && _currentChar == '.')) {
                    sb.Append(_currentChar);
                    haveDecimalPoint = _currentChar == '.';
                    NextChar();
                }

                // Parse it
                _number = double.Parse(sb.ToString(), CultureInfo.InvariantCulture);
                Token = Token.Number;
                CheckPrevToken();
                return;
            }

            // Identifier - starts with letter or underscore
            if (char.IsLetter(_currentChar) || _currentChar == '_') {
                var sb = new StringBuilder();

                // Accept letter, digit or underscore
                while (char.IsLetterOrDigit(_currentChar) || _currentChar == '_') {
                    sb.Append(_currentChar);
                    NextChar();
                }

                // Setup token
                _identifier = sb.ToString();
                Token = Token.Identifier;
                CheckPrevToken();
                return;
            }
        }

        private void CheckPrevToken()
        {
            if (PrevToken != Token.EOF && !AllowedPrevTokenMap[Token].Contains(PrevToken))
                throw new SyntaxException($"Unexpected token {Token} after {PrevToken}");
        }
    }
}