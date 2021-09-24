float3 hsvRgb(float3 hsv) {
    float3 ret;
    if (hsv.y == 0) { /* Grayscale */
        ret.x = ret.y = ret.z = hsv.z;// v

    }
    else {
        hsv.x -= floor(hsv.x);
        //if (1.0 <= hsv.x) hsv.x -= 1.0;
        hsv.x *= 6.0;
        float i = floor(hsv.x);
        float f = hsv.x - i;
        float aa = hsv.z * (1 - hsv.y);
        float bb = hsv.z * (1 - (hsv.y * f));
        float cc = hsv.z * (1 - (hsv.y * (1 - f)));
        if (i < 1) {
            ret.x = hsv.z; ret.y = cc;    ret.z = aa;

        }
        else if (i < 2) {
            ret.x = bb;    ret.y = hsv.z; ret.z = aa;

        }
        else if (i < 3) {
            ret.x = aa;    ret.y = hsv.z; ret.z = cc;

        }
        else if (i < 4) {
            ret.x = aa;    ret.y = bb;    ret.z = hsv.z;

        }
        else if (i < 5) {
            ret.x = cc;    ret.y = aa;    ret.z = hsv.z;

        }
        else {
            ret.x = hsv.z; ret.y = aa;    ret.z = bb;

        }

    }
    return ret;
}

float3 rgbHsv(float3 c)
{
    float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
    float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
    float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));

    float d = q.x - min(q.w, q.y);
    float e = 1.0e-10;
    return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
}
