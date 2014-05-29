
Shader "GLSL shader with sphere map" {
	Properties {
		_MainTex ("Texture Image", 2D) = "white" {} 
		_Color ("Overall Diffuse Color Filter", Color) = (0.2,0.2,0.2,1)
		_SpecColor ("Specular Material Color", Color) = (0.2,0.2,0.2,1)
		_Shininess ("Shininess", Float) = 10
	}
	SubShader {
		Pass {	
			 GLSLPROGRAM
 
				uniform sampler2D _MainTex;	
				uniform vec4 _MainTex_ST; 
				// tiling and offset parameters of property 
				uniform vec4 _Color;
				uniform vec4 _SpecColor;
				uniform float _Shininess;            
 
 				uniform vec4 _WorldSpaceLightPos0; // direction to or position of light source
 				//uniform vec3 _WorldSpaceCameraPos; //eyePosition?!?! maybe

 				uniform vec4 _LightColor0; // color of light source (from "Lighting.cginc")

				varying vec2 textureCoordinates;

				varying vec3 HalfVector;
				varying vec3 LightVector;
				varying vec3 Normal;
				varying float LightDistance;

				#ifdef VERTEX
 
					 void main() {

						vec3 N = normalize(vec3(gl_NormalMatrix * gl_Normal));

						vec4 position = gl_ModelViewMatrix * gl_Vertex;
						vec3 U = normalize(vec3(position));
						vec3 R = reflect(U, N);

						float m = 2.0 * sqrt(2*(R.z + 1));
						textureCoordinates = vec2(R.x / m + 0.5,  - R.y / m + 0.5); 

						Normal = N;
						vec3 eyePosistion = vec3(position);
						vec3 lightDirection = vec3(_WorldSpaceLightPos0 - eyePosistion); //L sem ser normalizado

						HalfVector = normalize(lightDirection + eyePosistion);
						LightVector = normalize(lightDirection);
						LightDistance = length(lightDirection);

						gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
					 }
 
				#endif
 
			 #ifdef FRAGMENT
 
				 void main() {
				 	float NdotH, NdotL;

				 	//vec3 ambientGlobal = 
				 	NdotL = max(0.0, dot(Normal, LightVector));
				 	vec3 ambient = vec3(_Color) * vec3(gl_LightModel.ambient);
				 	vec3 diffuse = vec3(_LightColor0) * vec3(_Color) * NdotL;
				 	vec3 specular;

				 	if(NdotL > 0.0){
				 		NdotH = max(0.0, dot(Normal, HalfVector));
				 		specular = vec3(_SpecColor) * vec3(_LightColor0) * pow(NdotH, _Shininess);
				 	}
				 	else{
				 		specular = vec3(0.0, 0.0, 0.0);
				 	}
				 	//vec3 auxText = vec3(texture2D(_MainTex, textureCoordinates), 0.0);
					//gl_FragColor = (diffuse + specular + ambient) * auxText;
					vec4 color = vec4(diffuse + specular + ambient, 0.0) * 5;
					gl_FragColor = texture2D(_MainTex, textureCoordinates) * color;

				 }
 
			 #endif
 
         ENDGLSL
      }
   }

}