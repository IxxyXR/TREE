using UnityEngine;
using System.Collections;

namespace TREESharp{
public class Genome{

	public float[] joints;
	public float[] divs;
	public float[] rads;
	public float[] length;
	public float[] angles;
	public float[] start;
	public float[] width;
	public float[] end;

}

public static class GenomeUtils {

	public static Hashtable fixGenome(params string[] genes){

		//helper function for generate

		Hashtable nGenome;
		if (genes.Length % 2 != 0) {
			Debug.LogWarning ("wrong number of arguments");
			return generateEmptyGenome(0);
		}
		else{
			int counter = 0;
			for(int i = 1 ; i < genes.Length ; i+=2){
				float[] t = TREEUtils.stringToFloatArray (genes [i]);
				if (counter < t.Length)
					counter = t.Length;
			}

			nGenome = generateEmptyGenome(counter);

			for(int i = 0 ; i < genes.Length ; i+=2){
				float[] a = TREEUtils.stringToFloatArray ((string)genes [i + 1]);
				float[] b = nGenome [(string)genes [i]] as float[];
				TREEUtils.mergeArrays (a,b);
			}
		}
		return nGenome;
	}

	public static float[] getValue(string s, Hashtable h){
		return h[s] as float[];
	}

	//convert hashtable into class

	public static Genome getGenome(Genome g, Hashtable genome){
		g.rads = GenomeUtils.getValue ("rads", genome);
		g.joints = GenomeUtils.getValue ("joints", genome);
		g.length = GenomeUtils.getValue ("length", genome);
		g.divs = GenomeUtils.getValue ("divs", genome);
		g.angles = GenomeUtils.getValue ("angles", genome);
		g.start = GenomeUtils.getValue ("start", genome);
		g.end = GenomeUtils.getValue ("end", genome);
		g.width = GenomeUtils.getValue ("width", genome);
		return g;
	}

	public static Hashtable generateEmptyGenome(int amount){
		Hashtable genome = new Hashtable ();
		float[] joints = new float[amount];
		TREEUtils.fillArray (6, joints);
		genome.Add ("joints", joints);
		float[] divs = new float[amount];
		TREEUtils.fillArray (1, divs);
		genome.Add ("divs", divs);
		genome.Add ("rads", new float[amount]);
		float[] length = new float[amount];
		TREEUtils.fillArray (1, length);
		genome.Add ("length", length);
		float[] angles = new float[amount];
		TREEUtils.fillArray (30, angles);
		genome.Add ("angles",angles);
		genome.Add ("start", new float[amount]);
		genome.Add ("width", new float[amount]);
		float[] end = new float[amount];
		TREEUtils.fillArray (-1, end);
		genome.Add ("end", end);
		return genome;

	}
}
}
