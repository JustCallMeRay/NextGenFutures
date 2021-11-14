Shader "Unlit/Noise_Maybe"
{
    Properties
    {
    _AngleOffset ("AngleOffset", Float) = 5
    _CellDensity ("CellDensity", Range(0.3,9)) = 1
    _MainTex ("Texture", 2D) = "white" {}
   // _Points ("Points (up to 9)", Float 3[9])          //I have no idea
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            //#include "UnityCG.cginc"      //often helpful but currently unneeded

            float _CellDensity;
            float _AngleOffset;
          // fixed3 Mask[6]; 
          static fixed3 Mask [7] = 
          {
                fixed3(1.0,0.0,0.0), 
                fixed3(0.0,1.0,0.0), 
                fixed3(0.0,0.0,1.0), 
                fixed3(1.0,1.0,0.0), 
                fixed3(0.0,1.0,1.0), 
                fixed3(1.0,0.0,1.0), 
                fixed3(1.0,1.0,1.0)
          };

          /*uniform const float3 points [9] =
          {
              float3 


          };*/  
          
            inline float2 unity_voronoi_noise_randomVector (float2 UV, float offset)
            {
                float2x2 m = float2x2(15.27, 47.63, 99.41, 89.98);
                UV = frac(sin(mul(UV, m)) * 46839.32);
                return float2(sin(UV.y*+offset)*0.5+0.5, cos(UV.x*offset)*0.5+0.5);
            }

            float2 WorleyNoise (float2 UV, float AngleOffset, float CellDensity)
           {
                float2 g = floor(UV * CellDensity);
                float2 f = frac(UV * CellDensity);
                float t = 8.0;
                float3 res = float3(8.0, 0.0, 0.0);
                float Out;
                float Cells;

                for(int y=-1; y<=1; y++) //cylces through nine points
                {
                    for(int x=-1; x<=1; x++)
                    {
                        float2 lattice = float2(x,y);
                        float2 offset = /*points.pointsArr [((y+1)*3)+x+1] = point;*/
                                        unity_voronoi_noise_randomVector(lattice + g, AngleOffset);
                        float d = distance(lattice + offset, f);
                        
                        if(d < res.x)
                        {
                           res = float3(d, offset.x, offset.y);
                           Out = res.x;
                           Cells = res.y;
                        }
                    }
                }

                return float2(Out, Cells);
           }

           struct u2v   //unity to vertex
           {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
           };

           
            struct v2f  //vertex to fragment
           {
                float2 uv : TEXCOORD0;
                //UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
           };

           
           
           v2f vert (u2v v)                    //**************VERTERX**************//
           {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
           }    

           fixed4 frag (v2f i) : SV_Target     //*************FRAGMENT***************// 
           {
                float3 colour = 0.;
                
                colour.xy = WorleyNoise(i.uv, _AngleOffset, _CellDensity);
                
                

                return float4(colour.yy,0,1);
           }

            ENDCG
}
}
}
/*

  ixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;


    void main() 
        {
        float2 st = gl_FragCoord.xy/u_resolution.xy;    //it hates "gl_FragCoord"
        st.x *= u_resolution.x/u_resolution.y;
        float3 color = float3(.0);

        // Scale
        st *= 3.;

        // Tile the space
        float2 i_st = floor(st);
        float2 f_st = fract(st);

        float m_dist = 1.;  // minimum distance

        for (int y= -1; y <= 1; y++)            //checks cells surrounding cells 
        {
            for (int x= -1; x <= 1; x++) 
            {
                // Neighbor place in the grid
                float2 neighbor = float2(float(x),float(y));

                // Random position from current + neighbor place in the grid
                float2 point = rand2(i_st + neighbor);
                
                //sets points to struct, allowing us to access them later.   //doesnt work on the fancy thing
                points.pointsArr[((y+1)*3)+x+1] = point;

			    // floattor between the pixel and the point
                float2 diff = neighbor + point - f_st;

                // Distance to the point
                float dist = length(diff);

                // Keep the closer distance
                m_dist = min(m_dist, dist);
            }
        }

        // Draw the min distance (distance field)
        color += m_dist;

        // Draw cell center
        //color += 1.-step(.02, m_dist);

        // Draw grid
        //color.r += step(.98, f_st.x) + step(.98, f_st.y);

        // Show isolines
        // color -= step(.7,abs(sin(27.0*m_dist)))*.5;      //Really fucking cool!

        Result = float4(color,1.0);                         //changed from gl_FragColor 
        }

        */

                        /*float2 i_st = floor(i.uv);
                float2 f_st = frac(i.uv);

                float m_dist = 1.;  // minimum distance

                for (int y= -1; y <= 1; y++)            //checks cells surrounding cells 
                {
                    for (int x= -1; x <= 1; x++) 
                    {
                        // Neighbor place in the grid
                        float2 neighbor = float2(float(x),float(y));

                        // Random position from current + neighbor place in the grid
                        float2 Point = Custom_create_rand2(i_st + neighbor);
                
                        //sets points to struct, allowing us to access them later. //doesnt work on the fancy thing
                        //points.pointsArr[((y+1)*3)+x+1] = Point;

			            // floattor between the pixel and the point
                        float2 diff = neighbor + Point - f_st;

                        // Distance to the point
                        float dist = length(diff);

                        // Keep the closer distance
                        m_dist = min(m_dist, dist);
                        col += m_dist;
                    }
                */