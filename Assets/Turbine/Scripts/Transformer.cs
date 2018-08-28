using UnityEngine;

namespace TREESharp
{
	[System.Serializable]
	public class Transformer
	{
		public Vector3 Rotation;  //rx, ry, rz,
		//OffsetRotation
		public Vector3 OffsetRotation;  //orx, ory, orz,
		//SinMultiply
		public float SinMultiply;  // sMult,
		//SinOffsetAxial
		public Vector3 SinOffsetAxialA;  // saorx, saory, saorz,
		public Vector3 SinOffsetAxialR;  // srorx, srory, srorz,
		//SinOffset
		public Vector3 SinOffsetR;  // sorx, sory, sorz,
		//SinFrequency
		public Vector3 SinFrequencyR;  // sfrx, sfry, sfrz,
		//SinSpeed
		public Vector3 SinSpeedR;  // ssrx, ssry, ssrz,
		//SinMultiply
		public Vector3 SinMultiplyR;  // smrx, smry, smrz,
		//SinUniformScale (true/false 1/0)
		public bool SinUniformScale;  // sus,
		public float Scale;  // scale,
		//ScaleXY
		public Vector3 ScaleXY;  // sx, sy, sz,
		//SinScaleMult
		public float SinScaleMult;  // ssMult,
		//SinOffsetFromRootOffset
		public Vector3 SinOffsetFromRootOffset;  // srosx, srosy, srosz,
		//SinOffset		 
		public Vector3 SinOffsetS;  // sosx, sosy, sosz,
		//SinFrequency	  
		public Vector3 SinFrequencyS;  // sfsx, sfsy, sfsz,
		//SinSpeed		  
		public Vector3 SinSpeedS;  // sssx, sssy, sssz,
		//SinMultiply	     
		public Vector3 SinMultiplyS;  // smsx, smsy, smsz,
		//NoiseJointMultiply
		public float NoiseJointMultiply;  // nMult,
		//NoiseRootOffset
		public Vector3 NoiseRootOffset;  // nrorx, nrory, nrorz,
		//NoiseOffsetAxial
		public Vector3 NoiseOffsetAxial;  // naorx, naory, naorz,
		//NoiseOffset
		public Vector3 NoiseOffset;  // norx, nory, norz,
		//NoiseFrequency	
		public Vector3 NoiseFrequency;  // nfrx, nfry, nfrz,
		//NoiseSpeed		
		public Vector3 NoiseSpeed;  // nsrx, nsry, nsrz,
		//NoiseMultiply	 
		public Vector3 NoiseMultiply;  // nmrx, nmry, nmrz,

		public float Length;  // length,

		//LengthMult
		public float LengthMult;  // lMult,
		//SinOffsetLength
		public float SinOffsetLength;  // sol,
		//SinFrequencyLength
		public float SinFrequencyLength;  // sfl,
		//SinSpeedLength
		public float SinSpeedLength;  // ssl,
		//SinAxisOffsetLength
		public float SinAxisOffsetLength;  // saol,
		//SinRootOffsetLength
		public float SinRootOffsetLength;  // srol,
		//SinMultiplyLength
		public float SinMultiplyLength;  // sml,

		//LengthColorMult
		public float LengthColorMult;  // cMult,
		//SinOffsetColor
		public float SinOffsetColor;  // soc,
		//SinFrequencyColor
		public float SinFrequencyColor;  // sfc,
		//SinSpeedColor
		public float SinSpeedColor;  // ssc,
		//SinAxisOffsetColor
		public float SinAxisOffsetColor;  // saoc,
		//SinRootOffsetColor
		public float SinRootOffsetColor;  // sroc,
		//SinMultiplyColor
		public float SinMultiplyColor;  // smc,
		//Color
		public Color SinColor;  // cr, cg, cb,
	}

//		public enum TransformType
//		{
//				rx, ry, rz,
//				//offset rotation
//				orx, ory, orz,
//				//sin mult
//				sMult,
//				//sin offset axial
//				saorx, saory, saorz,
//				srorx, srory, srorz,
//				//sin offset
//				sorx, sory, sorz,
//				//sin frequency
//				sfrx, sfry, sfrz,
//				//sin speed
//				ssrx, ssry, ssrz,
//				//sin multiply
//				smrx, smry, smrz,
//				//sin uniform scale (true/false 1/0)
//				sus,
//				scale,
//				//scale xyz
//				sx, sy, sz,
//				//sin scale mult
//				ssMult,
//				//sin offset from root offset
//				srosx, srosy, srosz,
//				//sin offset		  
//				sosx, sosy, sosz,
//				//sin frequency	  	
//				sfsx, sfsy, sfsz,
//				//sin speed		  	
//				sssx, sssy, sssz,
//				//sin multiply	      
//				smsx, smsy, smsz,
//				//noise joint multiply
//				nMult,
//				//noise root offset
//				nrorx, nrory, nrorz,
//				//noise offset axial
//				naorx, naory, naorz,
//				//noise offset
//				norx, nory, norz,
//				//noise frequency	 
//				nfrx, nfry, nfrz,
//				//noise speed		 
//				nsrx, nsry, nsrz,
//				//noise multiply	  
//				nmrx, nmry, nmrz,
//
//				length,
//
//				//length mult
//				lMult,
//				//sin offset length
//				sol,
//				//sin frequency length
//				sfl,
//				//sin speed length
//				ssl,
//				//sin axis offset length
//				saol,
//				//sin root offset length
//				srol,
//				//sin multiply length
//				sml,
//
//				//length color
//				cMult,
//				//sin offset color
//				soc,
//				//sin frequency color
//				sfc,
//				//sin speed color
//				ssc,
//				//sin axis offset color
//				saoc,
//				//sin root offset color
//				sroc,
//				//sin multiply color
//				smc,
//				//colors
//				cr, cg, cb,
//		}
}