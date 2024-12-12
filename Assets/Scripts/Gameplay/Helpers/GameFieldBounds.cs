using UnityEngine;

namespace Gameplay.Helpers
{
    public class GameFieldBounds : MonoBehaviour
    {
        [field: SerializeField] public Vector2 BordersOffset { get; private set; }

#if UNITY_EDITOR
    
        private void OnDrawGizmosSelected()
        {
            var cam = Camera.main;

            var screenPoint = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

            var maxY = (screenPoint.y - BordersOffset.y) * 2f; //TODO Visual settings, borders offsets
            var maxX = (screenPoint.x - BordersOffset.x) * 2;

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(maxX, maxY, 0.1f));
        }
#endif
    }
}