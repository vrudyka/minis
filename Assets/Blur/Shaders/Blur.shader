Shader "Unlit/Blur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _KernelSize("Kernel Size (N)", Range (1, 100)) = 30
        _Sigma("Sigma", Range(1, 20)) = 15
    }
    
    CGINCLUDE

    #include "UnityCG.cginc"

    struct appdata
    {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
    };

    struct v2f
    {
        float4 vertex : SV_POSITION;
        float2 uv : TEXCOORD0;
    };

    sampler2D _MainTex;
    int _KernelSize;
    int _Sigma;
    float4 _MainTex_TexelSize;

    float gaussian(float x, float sigma)
    {
        return exp(-0.5 * (x * x) / (sigma * sigma));
    }

    fixed4 frag_horizontal(v2f_img i) : SV_Target
    {
        fixed4 sum = fixed4(0.0, 0.0, 0.0, 0.0);
        int upper = ((_KernelSize - 1) / 2);
        int lower = -upper;
        float totalWeight = 0.0;

        for (int x = lower; x <= upper; ++x)
        {
            float weight = gaussian(x, _Sigma);
            totalWeight += weight;

	        sum += weight * tex2D(_MainTex, i.uv + fixed2(_MainTex_TexelSize.x * x, 0.0));
        }

        sum /= totalWeight;

        return sum;
    }

    fixed4 frag_vertical(v2f_img i) : SV_Target
    {
        fixed4 sum = fixed4(0.0, 0.0, 0.0, 0.0);
        int upper = ((_KernelSize - 1) / 2);
        int lower = -upper;
        float totalWeight = 0.0;

        for (int y = lower; y <= upper; ++y)
        {
            float weight = gaussian(y, _Sigma);
            totalWeight += weight;

	        sum += weight * tex2D(_MainTex, i.uv + fixed2(0.0, _MainTex_TexelSize.y * y));
        }

        sum /= totalWeight;

        return sum;
    }

    ENDCG

    SubShader
    {
        Pass
        {
            ZTest Always Cull Off ZWrite Off
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag_vertical
            ENDCG
        }
        Pass
        {
            ZTest Always Cull Off ZWrite Off
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag_horizontal
            ENDCG
        }
    }
}
