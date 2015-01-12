Cell Layout for AngularJS
=========================

A directive for Cell Layout, sort of like float in css but allows for cells of different heights ie float:up-left;

Usage
-----

Add the directive attributes to you container and cells

    <ul cell-layout-container>
        <li cell-layout-cell>

            ...

        </li>
    </ul>

Set the cell width using css

    .cell-layout-width  {
        width: 33.3333%; // will give you three columns
    }

*The directive will look out for window resizes and height changes in the cells to reset itself .. Yay!*

Example & Code
--------------

http://code.antix.co.uk/angularjs-components/#/cell-layout/
https://github.com/MrAntix/antix/tree/master/source/Antix.Code.Web/angularjs-components/cell-layout
