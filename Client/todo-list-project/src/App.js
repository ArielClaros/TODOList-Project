import React from 'react';
import './TaskList.css'; // Importa el archivo de estilos
import TaskList from './Components/TaskList';

function App() {
  return (
    <div className="todo-list">
      <TaskList />
    </div>
  );
}

export default App;
