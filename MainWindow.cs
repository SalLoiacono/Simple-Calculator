using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{
	// Initialize variables
	string buffer = ""; // Current value of entered number
	char op; // Operation needed to calculate result
	double[] number = new double[2]; // Contains both numbers to be calculated
	double result; // Contains result from both numbers to be calculated
	int step = 1; // Holds current step of process (Fill number 1 or 2)

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	// Function for number buttons
	protected void NumberButtons (object sender, EventArgs e)
	{
		// Create button and listen to which number is selected
		Button button = sender as Button;
		if ((buffer.Length == 0 && button.Label == "0") || (button == null))
			return;
		buffer = button.Label + buffer;
		// Write buffer to Text Box
		txtNumberView.Text = buffer;
	}

	// Function to make number negative
	protected void Negate (object sender, EventArgs e)
	{
		// Negate value, if button is pressed
		if (buffer.Contains ("-"))
			buffer = buffer.Replace ("-", "");
		else
			buffer = "-" + buffer;
		// Write buffer to Text Box
		txtNumberView.Text = buffer;
	}

	// Function to add a decimal value to the number
	protected void Dot (object sender, EventArgs e)
	{
		// Add decimal place to buffer, if button is pressed
		if (!buffer.Contains (".")) {
			buffer = buffer + ".";
			// Write buffer to Text Box
			txtNumberView.Text = buffer;
		}
	}

	// Function holding operations
	protected void Operations (object sender, EventArgs e)
	{
		// Create button and listen to which operation is selected
		Button button = sender as Button;
		op = button.Label[0];
		// If no current buffer, result equals number selected
		if (buffer.Length == 0)
			number[step - 1] = result;
		// Parse the buffer, if length of buffer is not 0
		else
			number[step - 1] = Double.Parse(buffer);
		// If on 2nd step, call the equal method, passing two nulls. Then set first number to result and step to 2.
		if (step == 2)
		{
			Equal(null, null);
			number[0] = result;
			step = 2;
		}
		// If the step value is anything else, set the number view to number[1] and increase the step.
		else
		{
			txtNumberView.Text = number[0].ToString();
			step++;
		}
		buffer = "";
	}

	// Function to calculate values
	protected void Equal (object sender, EventArgs e)
	{
		if (buffer.Length != 0)
			number[1] = Double.Parse(buffer);
		// Switch statement for different operations
		switch (op)
		{
		case '+': result = number[0] + number[1]; break;
		case '-': result = number[0] - number[1]; break;
		case '*': result = number[0] * number[1]; break;
		case '/': result = number[0] / number[1]; break;
		}
		// Write result to Text Box
		txtNumberView.Text = result.ToString();
		step = 1;
		buffer = "";
	}

	// Function to clear current buffer and numbers
	protected void Clear (object sender, EventArgs e)
	{
		// If clear is pressed once, clear the buffer
		if (buffer.Length == 0)
		{
			step = 1;
			number[0] = number[1] = 0.0;
			op = ' ';
			result = 0.0;
		}
		// If clear is pressed twice, clear both numbers 
		else
			buffer = "";
		// Write buffer to Text Box
		txtNumberView.Text = buffer;
	}
}
