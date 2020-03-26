using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

namespace Examples
{
    public class EnemyAttr
    {
        private Vector3 _targetPos;
        private Enemy _enemy;

        public float MoveEndTime { set; get; }

        public Vector3 TargetPos
        {
            set
            {
                _targetPos = value;
                NewTarget = true;
            }
            get => _targetPos;
        }

        public bool NewTarget { set; get; }

        public Vector3 Destination
        {
            set => _enemy.Agent.SetDestination(value);
            get => _enemy.Agent.destination;
        }

        public bool Stop
        {
            set
            {
                if (value)
                {
                    _enemy.Agent.enabled = false;
                    _enemy.Agent.enabled = true;
                }
            }
            get => _enemy.Agent.isStopped;
        }

        public EnemyAttr(Enemy enemy)
        {
            _enemy = enemy;
        }

        static EnemyAttr()
        {
            var properties = typeof(EnemyAttr).GetProperties();
            foreach (var prop in properties)
            {
                AllFields[prop.Name] = prop;
            }
        }

        public static Dictionary<string, PropertyInfo> AllFields = 
            new Dictionary<string, PropertyInfo>();


        public object GetValue(string propName)
        {
            if (!AllFields.ContainsKey(propName))
            {
                throw new Exception($"GetPropNotExist: {propName}");
            }
            return AllFields[propName].GetValue(this);
        }

        public void SetValue(string propName, object value)
        {
            if (!AllFields.ContainsKey(propName))
            {
                throw new Exception($"SetPropNotExist: {propName}");
            }
            AllFields[propName].SetValue(this, value);
        }
    }
}
