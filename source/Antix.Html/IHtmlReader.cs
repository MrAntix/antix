using System;

namespace Antix.Html
{
    public interface IHtmlReader
    {
        bool Any();
        char Peek();
        string Peek(int count);
        char Consume();
        string Consume(int count);
        void Consume(Func<char,bool> match);

        bool TryConsume(
            string target,
            bool seek, bool consumeTarget);

        bool TryConsume(
            string target,
            bool seek, bool consumeTarget,
            out string consumed);
    }
}