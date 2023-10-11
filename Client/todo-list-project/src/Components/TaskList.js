import React, { useState, useEffect } from "react";
import axios from "axios";

function TaskList() {
    const [tasks, setTasks] = useState([]);
    const [showPopup, setShowPopup] = useState(false);
    const [newTask, setNewTask] = useState({
        TaskName: "",
        StartDate: "",
        EndDate: "",
    });
    const [editingTask, setEditingTask] = useState(null); // Nuevo estado para rastrear la tarea en edición

    useEffect(() => {
        axios
            .get("https://todolist-dsw.azurewebsites.net/api/Tasks")
            .then((response) => {
                setTasks(response.data);
            })
            .catch((error) => {
                console.error("Error al obtener las tareas:", error);
            });
    }, []);

    const handleDelete = async (id) => {
        try {
            await axios.delete(`https://todolist-dsw.azurewebsites.net/api/Tasks/${id}`);
            setTasks(tasks.filter((task) => task.id !== id));
        } catch (error) {
            console.error("Error al eliminar la tarea:", error);
        }
    };

    const handleSave = async () => {
        try {
            const response = await axios.post(
                "https://todolist-dsw.azurewebsites.net/api/Tasks",
                newTask
            );
            setTasks([...tasks, response.data]);
            setShowPopup(false);
        } catch (error) {
            console.error("Error al crear la tarea:", error);
        }
    };

    const handleEdit = (task) => {
        setEditingTask(task);
        setShowPopup(true);
        setNewTask(task);
    };

    const handleUpdate = async () => {
        try {
            await axios.put(`https://todolist-dsw.azurewebsites.net/api/Tasks/${editingTask.id}`, newTask);
            const updatedTasks = tasks.map(task => task.id === editingTask.id ? newTask : task);
            setTasks(updatedTasks);
            setShowPopup(false);
        } catch (error) {
            console.error("Error al actualizar la tarea:", error);
        }
    };

    return (
        <div className="task-list-container">
            <h1>Lista de Tareas</h1>
            <div>
                {tasks.map((task) => (
                    <div className="task-item" key={task.id}>
                        <div className="task-details">{task.taskName}</div>
                        <div className="item-button">
                            <button onClick={() => handleEdit(task)}>Edit</button>
                            <button onClick={() => handleDelete(task.id)}>Delete</button>
                        </div>
                    </div>
                ))}
            </div>
            <div className="item-button-more">
                <button onClick={() => setShowPopup(true)}>+</button>
            </div>
            {showPopup && (
                <div className="popup">
                    <input
                        type="text"
                        placeholder="Name"
                        value={newTask.TaskName}
                        onChange={(e) =>
                            setNewTask({ ...newTask, TaskName: e.target.value })
                        }
                    />
                    <input
                        type="text"
                        placeholder="Start Date"
                        value={newTask.StartDate}
                        onChange={(e) =>
                            setNewTask({ ...newTask, StartDate: e.target.value })
                        }
                    />
                    <input
                        type="text"
                        placeholder="End Date"
                        value={newTask.EndDate}
                        onChange={(e) =>
                            setNewTask({ ...newTask, EndDate: e.target.value })
                        }
                    />
                    {editingTask ? (
                        <button onClick={handleUpdate}>Save</button>
                    ) : (
                        <button onClick={handleSave}>Save</button>
                    )}
                    <button onClick={() => setShowPopup(false)}>Cancel</button>
                </div>
            )}
        </div>
    );
}

export default TaskList;
