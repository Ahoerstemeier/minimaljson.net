using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A dictionary which cannot be modified.
/// </summary>
/// <typeparam name="TKey">Key type.</typeparam>
/// <typeparam name="TValue">Value type.</typeparam>
public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
{
    private IDictionary<TKey, TValue> _dictionary;

    #region constructor

    /// <summary>
    /// Creates a new instance of <see cref="ReadOnlyDictionary{T,V}"/>.
    /// </summary>
    public ReadOnlyDictionary()
    {
        _dictionary = new Dictionary<TKey, TValue>();
    }

    /// <summary>
    /// Creates a new instance of <see cref="ReadOnlyDictionary{T,V}"/> by copying the content of <paramref name="dictionary"/>.
    /// </summary>
    /// <param name="dictionary">Source dictionary.</param>
    /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> is <c>null</c>.</exception>
    public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
    {
        if ( dictionary == null )
        {
            throw new ArgumentNullException("dictionary");
        }
        _dictionary = dictionary;
    }

    #endregion constructor

    #region IDictionary<TKey,TValue> Members

    /// <summary>
    /// Adds an element with the provided key and value to the dictionary.
    /// </summary>
    /// <param name="key">The object to use as the key of the element to add.</param>
    /// <param name="value">The object to use as the value of the element to add.</param>
    /// <exception cref="ReadOnlyException">Dictionary is read only.</exception>
    void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
    {
        throw ReadOnlyException();
    }

    /// <summary>Determines whether the dictionary contains an element with the specified key.</summary>
    /// <param name="key">The key to locate.</param>
    /// <returns><c>true</c> if the dictionary containsan element with the key; otherwise, <c>false</c>.</returns>
    public Boolean ContainsKey(TKey key)
    {
        return _dictionary.ContainsKey(key);
    }

    /// <summary>
    /// Gets a collection containing the keys of the dictionary.
    /// </summary>
    /// <value>A collection containing the keys of the dictionary.</value>
    public ICollection<TKey> Keys
    {
        get
        {
            return _dictionary.Keys;
        }
    }

    /// <summary>
    /// Removes the element with the specified key from the dictionary.
    /// </summary>
    /// <param name="key">The key of the element to remove.</param>
    /// <returns><c>true</c> if the element is successfully removed; otherwise, <c>false</c>.</returns>
    /// <exception cref="ReadOnlyException">Dictionary is read only.</exception>
    Boolean IDictionary<TKey, TValue>.Remove(TKey key)
    {
        throw ReadOnlyException();
    }

    /// <summary>
    /// Gets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key whose value to get.</param>
    /// <param name="value">When this method returns, the value associated with the specified key, if
    /// the key is found; otherwise, the default value for the type of the value
    /// parameter. This parameter is passed uninitialized.</param>
    /// <returns>´<c>true</c> if the dictionary contains an element with the specified key; otherwise, <c>false</c>.</returns>
    public Boolean TryGetValue(TKey key, out TValue value)
    {
        return _dictionary.TryGetValue(key, out value);
    }

    /// <summary>
    /// Gets a collection containing the values of the dictionary.
    /// </summary>
    /// <value>A collection containing the values of the dictionary.</value>
    public ICollection<TValue> Values
    {
        get
        {
            return _dictionary.Values;
        }
    }

    /// <summary>
    /// Gets or sets the element with the specified key.
    /// </summary>
    /// <param name="key">The key of the element to get or set.</param>
    /// <returns>The element with the specified key.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
    /// <exception cref="KeyNotFoundException">The property is retrieved and key is not found.</exception>
    /// <exception cref="NotSupportedException">The property is set and the dictionary is read-only.</exception>
    public TValue this[TKey key]
    {
        get
        {
            return _dictionary[key];
        }
    }

    TValue IDictionary<TKey, TValue>.this[TKey key]
    {
        get
        {
            return this[key];
        }
        set
        {
            throw ReadOnlyException();
        }
    }

    #endregion IDictionary<TKey,TValue> Members

    #region ICollection<KeyValuePair<TKey,TValue>> Members

    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
    {
        throw ReadOnlyException();
    }

    void ICollection<KeyValuePair<TKey, TValue>>.Clear()
    {
        throw ReadOnlyException();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return _dictionary.Contains(item);
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        _dictionary.CopyTo(array, arrayIndex);
    }

    public int Count
    {
        get
        {
            return _dictionary.Count;
        }
    }

    public bool IsReadOnly
    {
        get
        {
            return true;
        }
    }

    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
    {
        throw ReadOnlyException();
    }

    #endregion ICollection<KeyValuePair<TKey,TValue>> Members

    #region IEnumerable<KeyValuePair<TKey,TValue>> Members

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return _dictionary.GetEnumerator();
    }

    #endregion IEnumerable<KeyValuePair<TKey,TValue>> Members

    #region IEnumerable Members

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion IEnumerable Members

    private static Exception ReadOnlyException()
    {
        return new NotSupportedException("This dictionary is read-only");
    }
}