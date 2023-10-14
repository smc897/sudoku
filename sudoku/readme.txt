This program solves sudoku. To play, go to the puzzles.txt in the build directory. The file looks like this:
2
0
800901020
203064000
006000007
100090000
008000960
000603708
095086271
041000003
082135640
1
047860000
050203004
006000090
504172608
301650429
802000070
020030000
000706950
005000306

The first line is the number of puzzles. The next is the 0, meaning puzzle 0, with the nine lines of content under. Then we have 1, which is puzzle 1, with 9 lines of content under it, and so on. To add a puzzle, add the last puzzle+1, then the content. Increment the count at the top of the file. 
