using System;
using System.Collections.Generic;

public delegate void MessageCallback();
public delegate void MessageCallback<T>(T t);
public delegate void MessageCallback<T, U>(T t, U u);

internal static class MessageManager
{
	private static Dictionary<string, Delegate> _listeners = new Dictionary<string, Delegate>();

	internal static void AddListener(string name, MessageCallback callback)
	{
		lock (_listeners)
		{
			if (!_listeners.ContainsKey(name))
				_listeners.Add(name, null);

			_listeners[name] = (MessageCallback)_listeners[name] + callback;
		}
	}

	internal static void RemoveListener(string name, MessageCallback callback)
	{
		if (!_listeners.ContainsKey(name))
			return;

		lock (_listeners)
		{
			_listeners[name] = (MessageCallback)_listeners[name] - callback;

			if (_listeners[name] == null)
				_listeners.Remove(name);
		}
	}

	internal static void Broadcast(string name)
	{
		Delegate d;
		if (!_listeners.TryGetValue(name, out d))
			return;

		if ((MessageCallback)d != null) ((MessageCallback)d)();
	}
}

internal static class MessageManager<T>
{
	private static Dictionary<string, Delegate> _listeners = new Dictionary<string, Delegate>();

	internal static void AddListener(string name, MessageCallback<T> callback)
	{
		lock (_listeners)
		{
			if (_listeners.ContainsKey(name))
				_listeners.Add(name, null);

			_listeners[name] = (MessageCallback<T>)_listeners[name] + callback;
		}
	}

	internal static void RemoveListener(string name, MessageCallback<T> callback)
	{
		if (!_listeners.ContainsKey(name))
			return;

		lock (_listeners)
		{
			_listeners[name] = (MessageCallback<T>)_listeners[name] - callback;

			if (_listeners[name] == null)
				_listeners.Remove(name);
		}
	}

	internal static void Broadcast(string name, T t)
	{
		Delegate d;
		if (!_listeners.TryGetValue(name, out d))
			return;

		if ((MessageCallback<T>)d != null) ((MessageCallback<T>)d)(t);
	}
}

internal static class MessageManager<T, U>
{
	private static Dictionary<string, Delegate> _listeners = new Dictionary<string, Delegate>();

	internal static void AddListener(string name, MessageCallback<T, U> callback)
	{
		lock (_listeners)
		{
			if (_listeners.ContainsKey(name))
				_listeners.Add(name, null);

			_listeners[name] = (MessageCallback<T, U>)_listeners[name] + callback;
		}
	}

	internal static void RemoveListener(string name, MessageCallback<T, U> callback)
	{
		if (!_listeners.ContainsKey(name))
			return;

		lock (_listeners)
		{
			_listeners[name] = (MessageCallback<T, U>)_listeners[name] - callback;

			if (_listeners[name] == null)
				_listeners.Remove(name);
		}
	}

	internal static void Broadcast(string name, T t, U u)
	{
		Delegate d;
		if (!_listeners.TryGetValue(name, out d))
			return;

		if ((MessageCallback<T, U>)d != null) ((MessageCallback<T, U>)d)(t, u);
	}
}