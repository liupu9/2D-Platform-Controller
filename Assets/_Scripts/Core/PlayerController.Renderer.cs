﻿

using UnityEngine;

namespace Myd.Platform
{
    /// <summary>
    /// Controller关于表现相关
    /// </summary>
    public partial class PlayerController
    {
        public float DashTrailTimer { get; set; }

        public static Vector2 NORMAL_SPRITE_SCALE = Vector2.one;
        public static Vector2 DUCK_SPRITE_SCALE = new Vector2(1F, 0.75f);

        //public Vector2 CurrSpriteScale { get; set; } = NORMAL_SPRITE_SCALE;

        public Color NormalHairColor = new Color32(0xAC, 0x32, 0x32, 0xFF);//00FFA2
        public Color UsedHairColor = new Color32(0x44, 0xB7, 0xFF, 0xFF);
        public Color FlashHairColor = Color.white;
        ////处理缩放
        //private void UpdateSprite(float deltaTime)
        //{
        //    float tempScaleX = Mathf.MoveTowards(Scale.x, CurrSpriteScale.x, 1.75f * deltaTime);
        //    float tempScaleY = Mathf.MoveTowards(Scale.y, CurrSpriteScale.y, 1.75f * deltaTime);
        //    this.Scale = new Vector2(tempScaleX, tempScaleY);
        //}

        //播放Dash特效
        public void PlayDashEffect(Vector3 position, Vector2 dir)
        {
            EffectControl.DashLine(position, dir);
            EffectControl.Ripple(position);
            EffectControl.CameraShake(dir);
        }

        public void PlayJumpEffect(Vector3 position)
        {
            SpriteControl.Scale(new Vector2(.6f, 1.4f));

            EffectControl.JumpDust(position);
            //蹬墙的粒子效果
        }

        public void PlayTrailEffect(int face)
        {
            SpriteControl.Trail(face);
        }

        public void PlayFallEffect(float ySpeed)
        {
            float half = Constants.MaxFall + (Constants.FastMaxFall - Constants.MaxFall) * .5f;
            if (ySpeed <= half)
            {
                float spriteLerp = Mathf.Min(1f, (ySpeed - half) / (Constants.FastMaxFall - half));
                Vector2 scale = Vector2.zero;
                scale.x = Mathf.Lerp(1f, 0.5f, spriteLerp);
                scale.y = Mathf.Lerp(1f, 1.5f, spriteLerp);
                SpriteControl.Scale(scale);
            }
        }

        public void PlayLandEffect(Vector3 position, float ySpeed)
        {
            float squish = Mathf.Min(ySpeed / Mathf.Abs(Constants.FastMaxFall), 1);
            float scaleX = Mathf.Lerp(1, 1.6f, squish);
            float scaleY = Mathf.Lerp(1, 0.4f, squish);
            SpriteControl.Scale(new Vector2(scaleX, scaleY));

            EffectControl.LandDust(position);
        }

        public void PlayDashFluxEffect(Vector2 dir, bool enable)
        {
            EffectControl.DashFlux(dir, enable);
        }

        public void PlayDuck(bool enable)
        {
            if (enable)
            {
                SpriteControl.Scale(new Vector2(1.4f, .6f));
                SpriteControl.SetSpriteScale(DUCK_SPRITE_SCALE);
            }
            else
            {
                if (this.OnGround && MoveY != 1)
                {
                    SpriteControl.Scale(new Vector2(.8f, 1.2f));
                }
                SpriteControl.SetSpriteScale(NORMAL_SPRITE_SCALE);
            }
        }

        public Vector3 SpritePosition { get => this.SpriteControl.SpritePosition; }
    }


}
