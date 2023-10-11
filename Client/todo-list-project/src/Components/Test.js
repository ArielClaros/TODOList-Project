import '../TaskList.css'; // Importa el archivo de estilos

function Test() {
    const numbers = Array.from({ length: 9 }, (_, i) => i + 1); // Crea un array de 1 a 9

  return (
    <div className="grid grid-cols-4 gap-4">
      {numbers.map(number => (
        <div key={number.toString()}>{number.toString().padStart(2, '0')}</div>
      ))}
    </div>
  );
}

export default Test;
