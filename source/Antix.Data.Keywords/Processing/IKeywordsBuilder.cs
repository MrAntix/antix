using System;
using System.Collections.Generic;

namespace Antix.Data.Keywords.Processing
{
    /// <summary>
    /// <para>Takes an entity and builds the keywords list</para>
    /// </summary>
    public interface IKeywordsBuilder
    {
        /// <summary>
        /// <para>Build the keywords</para>
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>An array of keywords</returns>
        string[] Build(object entity);
    }

    /// <summary>
    /// <para>Provides a fluent interface for building a list of keywords given an entity</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IKeywordsBuilder<T> :
        IKeywordsBuilder
    {
        /// <summary>
        /// <para>Provide a function to return a string to be used in the index</para>
        /// </summary>
        /// <param name="func">A function e.g. entity => entity.PropertyToIndex</param>
        IKeywordsBuilder<T> Index(
            Func<T, string> func);

        /// <summary>
        /// <para>Provide a function to access sub-objects to be used in the index</para>
        /// </summary>
        /// <param name="func">A function e.g. entity => entity.SubEntity</param>
        /// <param name="action">An action recieving a builder to index sub-objects</param>
        IKeywordsBuilder<T> For<TProp>(
            Func<T, TProp> func, Action<KeywordsBuilder<TProp>> action);

        /// <summary>
        /// <para>Provide a function to access sub-collctions to be used in the index</para>
        /// </summary>
        /// <param name="func">A function e.g. entity => entity.SubEntities</param>
        /// <param name="action">An action recieving a builder to index sub-objects</param>
        IKeywordsBuilder<T> ForEach<TProp>(
            Func<T, IEnumerable<TProp>> func, Action<KeywordsBuilder<TProp>> action);

        /// <summary>
        /// <para>Build the keywords</para>
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>An array of keywords</returns>
        string[] Build(T entity);
    }
}