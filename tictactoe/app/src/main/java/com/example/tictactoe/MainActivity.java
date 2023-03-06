package com.example.tictactoe;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.util.Log;
import android.widget.Toast;

public class MainActivity extends AppCompatActivity {

    private CellState nextMove = CellState.X;
    private CellState[][] gameState = new CellState[][]{
            {CellState.NONE, CellState.NONE, CellState.NONE},
            {CellState.NONE, CellState.NONE, CellState.NONE},
            {CellState.NONE, CellState.NONE, CellState.NONE},
    };

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        CellView cell00 = findViewById(R.id.cell00);
        cell00.setCoordinates(0, 0);
        CellView cell01 = findViewById(R.id.cell01);
        cell01.setCoordinates(0, 1);
        CellView cell02 = findViewById(R.id.cell02);
        cell02.setCoordinates(0, 2);
        CellView cell10 = findViewById(R.id.cell10);
        cell10.setCoordinates(1, 0);
        CellView cell11 = findViewById(R.id.cell11);
        cell11.setCoordinates(1, 1);
        CellView cell12 = findViewById(R.id.cell12);
        cell12.setCoordinates(1, 2);
        CellView cell20 = findViewById(R.id.cell20);
        cell20.setCoordinates(2, 0);
        CellView cell21 = findViewById(R.id.cell21);
        cell21.setCoordinates(2, 1);
        CellView cell22 = findViewById(R.id.cell22);
        cell22.setCoordinates(2, 2);

        CellView[] cellViews = new CellView[]{cell00, cell01, cell02, cell10, cell11, cell12, cell20, cell21, cell22};

        for (CellView cellView : cellViews) {
            cellView.setOnClickListener(v -> onCellClick((CellView) v));
        }
    }

    private void onCellClick(CellView cell) {
        int x = cell.getCellX();
        int y = cell.getCellY();

        if (gameState[y][x] != CellState.NONE) return;

        gameState[y][x] = nextMove;
        cell.setState(nextMove);

        checkWin();

        if (nextMove == CellState.X) {
            nextMove = CellState.O;
        } else {
            nextMove = CellState.X;
        }
    }

    private void checkWin() {
        int[][] xs = new int[][]{
                {0, 1, 2},
                {0, 1, 2},
                {0, 1, 2},
                {0, 0, 0},
                {1, 1, 1},
                {2, 2, 2},
                {0, 1, 2},
                {2, 1, 0},
        };
        int[][] ys = new int[][]{
                {0, 0, 0},
                {2, 2, 2},
                {2, 2, 2},
                {0, 1, 2},
                {0, 1, 2},
                {0, 1, 2},
                {0, 1, 2},
                {0, 1, 2},
        };

        for (int i = 0; i < xs.length; i++) {
            if (isThreeCellEqual(
                    gameState[ys[i][0]][xs[i][0]],
                    gameState[ys[i][1]][xs[i][1]],
                    gameState[ys[i][2]][xs[i][2]]
            )) {
                CellState winner = gameState[ys[i][0]][xs[i][0]];
                Toast.makeText(this, "Winner is " + winner, Toast.LENGTH_SHORT).show();
                return;
            }
        }
    }

    private boolean isThreeCellEqual(CellState cell1, CellState cell2, CellState cell3) {
        return cell1 == cell2 && cell2 == cell3 && cell1 != CellState.NONE;
    }

}