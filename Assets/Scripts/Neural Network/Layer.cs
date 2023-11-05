using System.Collections.Generic;

public class Layer {

    private readonly int numNodesIn, numNodesOut;

    private readonly double[,] weights;
    private readonly double[] biases;

    public Layer(int nodesIn, int nodesOut){
        numNodesIn = nodesIn;
        numNodesOut = nodesOut;

        weights = new double[numNodesIn, numNodesOut];
        biases = new double[numNodesOut];
    }

    public double[] CalculateOutputs(double[] inputs){
        double[] weightedInputs = new double[numNodesOut];

        for (int nodeOut = 0; nodeOut < numNodesOut; nodeOut++){
            double weightedInput = biases[nodeOut];

            for (int nodeIn = 0; nodeIn < numNodesIn; nodeIn++){
                weightedInput += inputs[nodeIn] * weights[nodeIn, nodeOut];
            }

            weightedInputs[nodeOut] = weightedInput;
        }

        return weightedInputs;
    }

    public double[] GetWeights() {
        double[] flattenedArray = new double[numNodesIn * numNodesOut];
        int index = 0;

        for (int i = 0; i < numNodesIn; i++)
        {
            for (int j = 0; j < numNodesOut; j++)
            {
                flattenedArray[index] = weights[i, j];
                index++;
            }
        }

        return flattenedArray;
    }

    public double[] GetBiases() {
        return biases;
    }
    
}
