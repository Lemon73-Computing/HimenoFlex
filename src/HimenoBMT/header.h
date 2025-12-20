extern "C"
{
    int main(double* out1, double* out2, int* out3, float* out4, double* out5, double* out6, double* out7);
    void initmt();
    float jacobi(int nn);
    double fflop(int mx, int my, int mz);
    double mflops(int nn, double cpu, double flop);
    double second();
}
