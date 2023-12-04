using System;
using UnityEngine;
using Zenject;

namespace GameArea
{
    public class GameAreaView: MonoBehaviour, IDisposable
    {
        public class Factory: PlaceholderFactory<GameAreaView>
        {
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}