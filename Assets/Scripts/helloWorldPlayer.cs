using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        public NetworkVariable<Vector2> Position = new NetworkVariable<Vector2>();

        public override void OnNetworkSpawn()
        {
            Position.OnValueChanged += OnStateChanged;

            if (IsOwner)
            {
                Move();
            }
        }

        public override void OnNetworkDespawn()
        {
            Position.OnValueChanged -= OnStateChanged;
        }

        public void OnStateChanged(Vector2 previous, Vector2 current)
        {
            // note: `Position.Value` will be equal to `current` here
            if (Position.Value != previous)
            {
                transform.position = Position.Value;
            }
        }

        public void Move()
        {
            SubmitPositionRequestServerRpc();
        }

        [Rpc(SendTo.Server)]
        void SubmitPositionRequestServerRpc(RpcParams rpcParams = default)
        {
            var randomPosition = GetRandomPositionOnPlane();
            transform.position = randomPosition;
            Position.Value = randomPosition;
        }

        static Vector2 GetRandomPositionOnPlane()
        {
            return new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        }
    }
}