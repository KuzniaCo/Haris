import gzip
import random
import sys

afile = gzip.open("/mnt/d/Downloads/movies.txt.gz")
for line in afile:
    sys.stdout.write(line)
afile.close()
