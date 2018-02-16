using UnityEngine;

public class PolygonShadowCaster : ShadowCaster {

    public override void CreateShadow(LightCalculator lightCalculator)
    {
        if(lightCalculator is PointLightCalculator)
            shadow = ShadowPolygonHelper.CreateShadowGameObject(gameObject, lightCalculator.Position, lightCalculator.Plane);
        else{
            shadow = ShadowPolygonHelper.CreateDirectionalShadowGameObject(gameObject, lightCalculator.Direction, lightCalculator.Plane);
        }   
    }
}
