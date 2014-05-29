Shader "Custom/BumpMapping" {
	Properties {
		_MainTex ("Texture Image", 2D) = "white" {}
		_BumpMap ("Bumpmap", 2D) = "bump" {}
		_Color ("Overall Diffuse Color Filter", Color) = (0.2,0.2,0.2,1)
		_SpecColor ("Specular Material Color", Color) = (0.2,0.2,0.2,1)
		_Shininess ("Shininess", Float) = 5
	}
	SubShader {
		Pass {
			Tags { "LightMode" = "ForwardBase" }

			GLSLPROGRAM
			uniform sampler2D _MainTex;
			uniform sampler2D _BumpMap;

			uniform vec4 _BumpMap_ST;
			// a uniform variable refering to the property above
			// (in fact, this is just a small integer specifying a
			// "texture unit", which has the texture image "bound"
			// to it; Unity takes care of this).

			uniform vec4 _Color;
			uniform vec4 _SpecColor;
			uniform float _Shininess;

			uniform vec3 _WorldSpaceCameraPos; // camera position in world space
			uniform mat4 _Object2World; // model matrix
			uniform mat4 _World2Object; // inverse model matrix
			uniform vec4 _WorldSpaceLightPos0; // direction to or position of light source
			uniform vec4 _LightColor0; // color of light source (from "Lighting.cginc")

			varying vec4 textureCoordinates; //coordenadas das texturas (UV)
			varying vec4 position; // posicao do vertice no "world space"
			varying mat3 localSurface2World; //matriz TBN
			varying vec3 LightVector;
			varying vec3 HalfVector;
			varying float attenuation;

			#ifdef VERTEX

			attribute vec4 Tangent;

			void main(){
				vec3 lightDirection;
				vec3 halfVector_aux;

				mat4 modelMatrix = _Object2World; //model matrix
				mat4 modelMatrixInverse = _World2Object;  //inversa da model matrix
				textureCoordinates = gl_MultiTexCoord0; //coordenadas da textura

				position = modelMatrix * gl_Vertex; //gl_ModelViewMatrix * gl_Vertex;

				//calculo da matriz TBN
	        	localSurface2World[0] = normalize(vec3 (modelMatrix * vec4(vec3(Tangent), 0.0))); //T
	        	localSurface2World[2] = normalize(vec3 (vec4(gl_Normal, 0.0) * modelMatrixInverse)); // N
	        	localSurface2World[1] = normalize(cross(localSurface2World[2], localSurface2World[0]) * Tangent.w); // B


	        	if(0.0 == _WorldSpaceLightPos0.w){
	        		attenuation = 1.0;
	        		lightDirection = normalize(vec3(_WorldSpaceLightPos0));
	        	} else {
	        		vec3 vertexToLightSource = vec3(_WorldSpaceLightPos0 - position);
	        		float distance = length(vertexToLightSource);
	        		attenuation = 1.0 / distance; //linear attenuation
	        		lightDirection = normalize(vertexToLightSource);
	        	}


				LightVector.x = dot(lightDirection, localSurface2World[0]);
				LightVector.y = dot(lightDirection, localSurface2World[1]);
				LightVector.z = dot(lightDirection, localSurface2World[2]);

	        	position = normalize(position);
	        	halfVector_aux = normalize(vec3(position) + lightDirection);
				HalfVector.x = dot(halfVector_aux, localSurface2World[0]);
				HalfVector.y = dot(halfVector_aux, localSurface2World[1]);
				HalfVector.z = dot(halfVector_aux, localSurface2World[2]);



				gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;			
			}
			
			#endif
			
			#ifdef FRAGMENT
			void main()	{
				float NdotH;
				vec4 textureColor = texture2D(_MainTex, textureCoordinates.xy);
				vec4 textureNormal = texture2D(_BumpMap, _BumpMap_ST.xy * textureCoordinates.xy + _BumpMap_ST.zw);

				vec3 lightVec = normalize(-LightVector);
				vec3 halfVec = normalize(HalfVector);

				vec3 N = vec3(2.0 * textureNormal.ag - vec2(1.0), 0.0);

				vec3 ambientLighting = vec3(gl_LightModel.ambient) * vec3(_Color);

				float NdotL = max(0.0, dot(N, lightVec));
				vec3 diffuseReflection = attenuation * vec3(_LightColor0) * vec3(_Color) * NdotL;
				vec3 specularReflection;
				if(NdotL < 0.0){
					specularReflection = vec3(0.0, 0.0, 0.0);
				} else{
					NdotH = max(0.0, dot(N, halfVec));
					specularReflection = attenuation * vec3(_LightColor0) * vec3(_SpecColor) * pow(NdotH, _Shininess);
				}

				gl_FragColor = vec4(ambientLighting + diffuseReflection + specularReflection, 1.0) * textureColor;
			}
			
			#endif
			ENDGLSL
		}

		Pass{
			Tags{ "LightMode" = "ForwardAdd"}
			Blend One One

			GLSLPROGRAM
			uniform sampler2D _MainTex;
			uniform sampler2D _BumpMap;

			uniform vec4 _BumpMap_ST;
			// a uniform variable refering to the property above
			// (in fact, this is just a small integer specifying a
			// "texture unit", which has the texture image "bound"
			// to it; Unity takes care of this).

			uniform vec4 _Color;
			uniform vec4 _SpecColor;
			uniform float _Shininess;

			uniform vec3 _WorldSpaceCameraPos; // camera position in world space
			uniform mat4 _Object2World; // model matrix
			uniform mat4 _World2Object; // inverse model matrix
			uniform vec4 _WorldSpaceLightPos0; // direction to or position of light source
			uniform vec4 _LightColor0; // color of light source (from "Lighting.cginc")

			varying vec4 textureCoordinates; //coordenadas das texturas (UV)
			varying vec4 position; // posicao do vertice no "world space"
			varying mat3 localSurface2World; //matriz TBN
			varying vec3 LightVector;
			varying vec3 HalfVector;
			varying float attenuation;

			#ifdef VERTEX

			attribute vec4 Tangent;

			void main(){
				vec3 lightDirection;
				vec3 halfVector_aux;

				mat4 modelMatrix = _Object2World; //model matrix
				mat4 modelMatrixInverse = _World2Object;  //inversa da model matrix
				textureCoordinates = gl_MultiTexCoord0; //coordenadas da textura

				position = modelMatrix * gl_Vertex; //gl_ModelViewMatrix * gl_Vertex;

				//calculo da matriz TBN
	        	localSurface2World[0] = normalize(vec3 (modelMatrix * vec4(vec3(Tangent), 0.0))); //T
	        	localSurface2World[2] = normalize(vec3 (vec4(gl_Normal, 0.0) * modelMatrixInverse)); // N
	        	localSurface2World[1] = normalize(cross(localSurface2World[2], localSurface2World[0]) * Tangent.w); // B


	        	if(0.0 == _WorldSpaceLightPos0.w){
	        		attenuation = 1.0;
	        		lightDirection = normalize(vec3(_WorldSpaceLightPos0));
	        	} else {
	        		vec3 vertexToLightSource = vec3(_WorldSpaceLightPos0 - position);
	        		float distance = length(vertexToLightSource);
	        		attenuation = 1.0 / distance; //linear attenuation
	        		lightDirection = normalize(vertexToLightSource);
	        	}


				LightVector.x = dot(lightDirection, localSurface2World[0]);
				LightVector.y = dot(lightDirection, localSurface2World[1]);
				LightVector.z = dot(lightDirection, localSurface2World[2]);

	        	position = normalize(position);
	        	halfVector_aux = normalize(vec3(position) + lightDirection);
				HalfVector.x = dot(halfVector_aux, localSurface2World[0]);
				HalfVector.y = dot(halfVector_aux, localSurface2World[1]);
				HalfVector.z = dot(halfVector_aux, localSurface2World[2]);



				gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;			
			}
			
			#endif

			#ifdef FRAGMENT
			void main()	{
				float NdotH;
				vec4 textureColor = texture2D(_MainTex, textureCoordinates.xy);
				vec4 textureNormal = texture2D(_BumpMap, _BumpMap_ST.xy * textureCoordinates.xy + _BumpMap_ST.zw);

				vec3 lightVec = normalize(-LightVector);
				vec3 halfVec = normalize(HalfVector);

				vec3 N = vec3(2.0 * textureNormal.ag - vec2(1.0), 0.0);

				//vec3 ambientLighting = vec3(gl_LightModel.ambient) * vec3(_Color);

				float NdotL = max(0.0, dot(N, lightVec));
				vec3 diffuseReflection = attenuation * vec3(_LightColor0) * vec3(_Color) * NdotL;
				vec3 specularReflection;
				if(NdotL < 0.0){
					specularReflection = vec3(0.0, 0.0, 0.0);
				} else{
					NdotH = max(0.0, dot(N, halfVec));
					specularReflection = attenuation * vec3(_LightColor0) * vec3(_SpecColor) * pow(NdotH, _Shininess);
				}

				gl_FragColor = vec4(diffuseReflection + specularReflection, 1.0) * textureColor;
			}
			
			#endif
			ENDGLSL
		}
		
	} //end subshader
// The definition of a fallback shader should be commented out
// during development:
// Fallback "Specular"
}