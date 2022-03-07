using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Actors.FlappyBird
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Building : MonoBehaviour
    {
        private SpriteRenderer _parentRender;
        private List<GameObject> _buildingsSprites = new List<GameObject>();

        public void Init((Sprite sprite, Vector2 position)[] buildingsSprites)
        {
            _parentRender = gameObject.GetComponent<SpriteRenderer>();
            
            foreach (var buildingSprite in buildingsSprites)
            {
                var building = new GameObject($"{buildingSprite.sprite.name}") {
                    transform = {
                        position = transform.position,
                        parent = transform
                    }
                };
                building.transform.position += (Vector3)buildingSprite.position;
                
                var buildRender = building.AddComponent<SpriteRenderer>();
                buildRender.sortingLayerID = _parentRender.sortingLayerID;
                buildRender.sortingOrder = _parentRender.sortingOrder;
                buildRender.sprite = buildingSprite.sprite;
                
                _buildingsSprites.Add(building);
            }
        }
    }
}