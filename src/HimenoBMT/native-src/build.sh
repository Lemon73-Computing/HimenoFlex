# Using Linux

# sudo apt install make mingw-w64
# (x86_64-w64-mingw32-gcc)

for s in SSMALL SMALL MIDDLE LARGE ELARGE; do
  # Linux
  # gcc -shared -D$s -fPIC himenoBMTxps.c -o himenoBMT_$s.so

  # Windows
  x86_64-w64-mingw32-gcc -shared -D$s himenoBMTxps.c -o himenoBMT_$s.dll
  mv himenoBMT_$s.dll ../runtimes/win-x64/native/

  # macOS
  # clang -dynamiclib himenoBMTxps.c -o himenoBMT_.dylib

  # iOS
  # clang -arch arm64 -c himenoBMTxps.c -o himenoBMTxps.o
  # libtool -static himenoBMTxps.o -o himenoBMT_.a

  # Android 
  # arm64-v8a
  # aarch64-linux-android24-clang \
  #   -shared -fPIC -D$s himenoBMTxps.c -o himenoBMT_$s.so
  # mv himenoBMT_$s.so ../runtimes/android-arm64-v8a/native/

  # armeabi-v7a
  # armv7a-linux-androideabiXX-clang \
  #   -shared -fPIC -D$s himenoBMTxps.c -o himenoBMT_$s.so
  # mv himenoBMT_$s.so ../runtimes/android-armeabi-v7a/native/

  # x64
  # x86_64-linux-androidXX-clang \
  #   -shared -fPIC -D$s himenoBMTxps.c -o himenoBMT_$s.so
  # mv himenoBMT_$s.so ../runtimes/android-x64/native/

done
