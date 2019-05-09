using System;
using System.Collections.Generic;
using UnityEngine;
using BlitheFramework;

public class FactoryPlatform: BaseClass
{
    #region EVENT
    public event EventHandler EVENT_REMOVE;
    #endregion EVENT
    private List<Platform> listOfObjetFactories;
    public FactoryPlatform()
    {
        listOfObjetFactories = new List<Platform>();
        Init();
    }
    public override void Init()
    {

    }

    public void Add(GameObject _object, Vector3 _position, Quaternion _rotation, float _speed)
    {
        Platform platform = new Platform();
        platform = Instantiate(_object, _position, _rotation).AddComponent<Platform>() as Platform;
        platform.Init(_speed);
        #region EVENT_LISTENER_ADD_Platform
        platform.GetComponent<Platform>().EVENT_REMOVE += Remove;
        #endregion EVENT_LISTENER_ADD_Platform
        listOfObjetFactories.Add(platform);
    }
    public Platform Get(int _indexObjectOnList)
    {
        return (listOfObjetFactories[_indexObjectOnList]) as Platform;
    }
    public void RemoveObjectFactories(int _indexObjectOnList)
    {
       Get(_indexObjectOnList).Remove();
    }
    public int GetNumberOfObjectFactories()
    {
       return listOfObjetFactories.Count;
    }
    #region EVENT_LISTENER_METHOD
    private void Remove(object _sender, EventArgs e)
    {
        GameObject sender = (GameObject)_sender;
        listOfObjetFactories.Remove(sender.GetComponent<Platform>());
        #region EVENT_LISTENER_REMOVE_Platform
        sender.GetComponent<Platform>().EVENT_REMOVE -= Remove;
        #endregion EVENT_LISTENER_REMOVE_Platform
        Destroy(sender);
    }
    #endregion EVENT_LISTENER_METHOD
    private void RemoveAllObjectFactories()
    {
        while (GetNumberOfObjectFactories() != 0)
        {
            RemoveObjectFactories(0);
        }
    }
    #region public method
    public void Remove()
    {
       RemoveAllObjectFactories();
       dispatchEvent(EVENT_REMOVE, this.gameObject, EventArgs.Empty);
    }
    #endregion
}